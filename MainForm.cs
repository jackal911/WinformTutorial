using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CanvasModel;
using System.IO;
using Canvas;

namespace GenericView
{
    public partial class MainForm : Form
    {
        #region 맴버
        #endregion

        #region 속성
        #endregion
        
        #region 생성자
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            string path = @"C:\Users\sgkim\Desktop\이준호\GenericView\Sample\AD1P-BL6A-1-1P.gen";
            string[] f = File.ReadAllLines(path);
            
            TreeNode rootNode = new TreeNode("root");
            int index = 0; // ref에 얹기 위해 초기화. 여기선 쓸모없고 내부에서 사용 목적
            int id = 0; // // ref에 얹기 위해 초기화. 여기선 쓸모없고 내부에서 사용 목적
            addNode(f, ref rootNode, ref index, ref id);
            treeView1.Nodes.Add(rootNode);
            //printAllNodes(rootNode);
            //findNodeByText(rootNode, "CONTOUR");
            parseAndDraw(rootNode);
        }

        class nodeTag
        {
            public int id { get; set; }
        }

        // 제네릭 파일 조회해서 트리만드는 함수
        void addNode(string[] lines, ref TreeNode parentNode, ref int index, ref int id)
        {
            while (index < lines.Length)
            {
                string sbj = lines[index];
                index++;
                if(!sbj.Contains('='))
                {                    
                    if (sbj.StartsWith("END_OF_"))
                    {
                        break;
                    }
                    else
                    {
                        sbj = sbj.Replace("START_OF_", "");
                        sbj = sbj.Replace("_DATA", "");
                        TreeNode childNode = new TreeNode(sbj);
                        if(sbj=="CONTOUR"||sbj=="STRING")
                        {
                            nodeTag tag = new nodeTag();
                            tag.id = id;
                            childNode.Tag = tag;
                        }
                        addNode(lines, ref childNode, ref index, ref id);
                        parentNode.Nodes.Add(childNode);
                    }
                }
                else
                {
                    TreeNode childNode = new TreeNode(sbj);
                    if (parentNode.Text == "CONTOUR" || parentNode.Text == "STRING")
                    {
                        nodeTag tag = new nodeTag();
                        tag.id = id;
                        childNode.Tag = tag;
                        if (sbj.StartsWith("V=") || sbj.StartsWith("STRING="))
                        {
                            id++;
                        }
                    }
                    parentNode.Nodes.Add(childNode);
                }
            }
        }

        // 데이터 제목 찾아서 하위 노드들 콘솔에 찍는 함수 - Built-in에는 Key로 조회하는 함수만 있어서 따로 구현
        void printNodeByText(TreeNode parentNode, string nodeText)
        {
            foreach (TreeNode node in parentNode.Nodes)
            {
                if (node.Text == nodeText)
                {
                    foreach (TreeNode nod in node.Nodes)
                    {
                        Console.WriteLine(nod.Text);
                    }
                    Console.WriteLine("--------------------------");
                }
                else
                {
                    printNodeByText(node, nodeText);
                }
            }
        }

        // CONTOUR의 node text로 entity를 잘라내 list로 리턴하는 함수
        List<double> findEntityByIndex(TreeNode parentNode, int index) 
        {
            List<double> tnList = new List<double>();
            bool isWhatImTalkin = false;
            foreach (TreeNode node in parentNode.Nodes)
            {
                if (node.Index == index)
                {
                    isWhatImTalkin = true;
                }
                if (node.Text.StartsWith("START_U=") || node.Text.StartsWith("U=") || node.Text.StartsWith("START_V=") || node.Text.StartsWith("V="))
                {
                    tnList.Add(double.Parse(node.Text.Split('=')[1]));
                }
                
                if (node.Text.StartsWith("V="))
                {
                    if (isWhatImTalkin)
                    {
                        break;
                    }
                    else
                    {
                        tnList.RemoveAt(0);
                        tnList.RemoveAt(0);
                    }
                }
            }
            return tnList;
        }

        // 트리 전체 조회하는 함수
        void printAllNodes(TreeNode parentNode)
        {
            foreach (TreeNode node in parentNode.Nodes)
            {
                Console.WriteLine(node.Text);
                printAllNodes(node);
            }
            if (parentNode.Nodes.Count != 0)
            {
                Console.WriteLine("END_OF_{0}", parentNode.Text);
            }
        }

        // 모든 CONTOUR와 STRING을 그리는 함수
        void parseAndDraw(TreeNode parentNode)
        {
            foreach (TreeNode node in parentNode.Nodes)
            {
                if (node.Text == "CONTOUR")
                {
                    List<Dictionary<string, double>> points = new List<Dictionary<string, double>>();
                    points.Add(new Dictionary<string, double>());
                    int index = 0; // 다루고있는 딕셔너리의 주소를 가리키는 index
                    foreach (TreeNode nod in node.Nodes)
                    {
                        if (!nod.Text.Contains('=')) // bevel같이 하위노드를 가지는 노드는 =을 안가져서 뺀다.
                        {
                            continue;
                        }
                        string[] kvPair = nod.Text.Split('='); // key와 value
                        if (kvPair[0]=="AMP_U")
                        {
                            points.Add(new Dictionary<string, double>());
                            index++;                            
                        }
                        points[index].Add(kvPair[0], double.Parse(kvPair[1]));
                    }
                    drawLines(points);
                }
                else if (node.Text == "STRING")
                {
                    Dictionary<string, string> properties = new Dictionary<string, string>();
                    foreach (TreeNode nod in node.Nodes)
                    {
                        string[] kvPair = nod.Text.Split('='); // key와 value
                        properties.Add(kvPair[0], kvPair[1]);
                    }
                    writeString(properties);
                }
                else
                {
                    parseAndDraw(node);
                }
            }
        }

        // list 받아서 그림그리는 함수
        void drawLines(List<Dictionary<string, double>> points)
        {
            double u = points[0]["START_U"];
            double v = points[0]["START_V"];
            int i=1;
            while(i<points.Count)
            {
                if (points[i]["RADIUS"] == 0)
                {
                    //ui_canvas2D.DrawCanvas.SelectEntity();
                    Console.WriteLine(ui_canvas2D.DrawCanvas.AddLine(u, v, points[i]["U"], points[i]["V"]));
                }
                else
                {
                    if (points[i]["AMP"] < 0)
                    {
                        Console.WriteLine(ui_canvas2D.DrawCanvas.AddArc(points[i]["ORIGIN_U"], points[i]["ORIGIN_V"], -points[i]["RADIUS"], u, v, points[i]["U"], points[i]["V"]));
                    }
                    else
                    {
                        Console.WriteLine(ui_canvas2D.DrawCanvas.AddArc(points[i]["ORIGIN_U"], points[i]["ORIGIN_V"], points[i]["RADIUS"], u, v, points[i]["U"], points[i]["V"]));
                    }                    
                }
                u = points[i]["U"];
                v = points[i]["V"];
                i += 1;
            }
        }

        // string데이터 삽입
        void writeString(Dictionary<string, string> properties)
        {
            Console.WriteLine(ui_canvas2D.DrawCanvas.AddText(double.Parse(properties["STRING_POSITION_U"]), double.Parse(properties["STRING_POSITION_V"]), double.Parse(properties["STRING_HEIGHT"]), double.Parse(properties["STRING_ANGLE"]), 0, properties["STRING"]));            
        }
        
        // 노드 선택하면 캔버스에 해당 노드 색이 빨간색으로 변하는 함수
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // 1. TreeNode Contour ->  시작 좌표  U, V
            // 2. 화면에 있는 부재 형상을 가져와서
            // 3. U,V 동일한 시작점을 가진 Entity을 찾아서 선택함
            int hDC = ui_canvas2D.DrawCanvas.GetHdc();
            if (e.Node.Parent == null || e.Node.Parent.Text != "CONTOUR")
            {
                ui_canvas2D.DrawCanvas.DeselectAll(hDC);
                return;
            }

            List<double> points = findEntityByIndex(e.Node.Parent, e.Node.Index);
            List<Entity> entityAllList = ui_canvas2D.DrawCanvas.ViewModel.ToList();
            Entity selectedEntity = null;
            Console.WriteLine("x : {0}, y : {1}", points[0], points[1]);
            foreach (Entity entity in entityAllList)
            {
                // Console.WriteLine("entity sx : {0}, sy : {1}", entity.Sx, entity.Sy);
                // U,V와 동일한 Entity Break
                if (entity.Sx == points[0] && entity.Sy == points[1])
                {
                    selectedEntity = entity;
                    break;
                }
            }

            ui_canvas2D.DrawCanvas.DeselectAll(hDC);
            ui_canvas2D.DrawCanvas.SelectEntity(hDC, selectedEntity);
            ui_canvas2D.DrawCanvas.ReleaseHdc(hDC);
        }

        #region Event
        #endregion
    }
}
