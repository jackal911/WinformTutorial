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
			string path = @"C:\Users\sgkim\Desktop\이준호\GenericView\Sample\AD1P-DK1A-2-1P.gen";
            string[] f = File.ReadAllLines(path);
            
            TreeNode rootNode = new TreeNode("root");
            int index = 0; // ref에 얹기 위해 초기화. 외부에서는 의미가 없다
            addNode(f, ref rootNode, ref index);
            treeView1.Nodes.Add(rootNode);
            printAllNodes(rootNode);
            parseAndDraw(rootNode);
        }

        // 노드 선택하면 캔버스에 해당 노드 색이 빨간색으로 변한다.
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int hDC = ui_canvas2D.DrawCanvas.GetHdc();
            if (e.Node.Parent == null || (e.Node.Parent.Text != "CONTOUR" && e.Node.Parent.Text != "STRING"))
            {
                ui_canvas2D.DrawCanvas.DeselectAll(hDC);
                return;
            }

            List<Entity> entityAllList = ui_canvas2D.DrawCanvas.ViewModel.ToList();
            Entity selectedEntity = null;
            List<double> points;
            if (e.Node.Parent.Text == "CONTOUR")
            {
                points = findContourEntityByIndex(e.Node.Parent, e.Node.Index);
                foreach (Entity entity in entityAllList)
                {
                    if (entity.Sx == points[0] && entity.Sy == points[1])
                    {
                        selectedEntity = entity;
                        break;
                    }
                }
            }
            else
            {
                points = findStringEntity(e.Node.Parent);
                foreach (Entity entity in entityAllList)
                {
                    if (entity.Px == points[0] && entity.Py == points[1])
                    {
                        selectedEntity = entity;
                        break;
                    }
                }
            }

            Console.WriteLine("x : {0}, y : {1}", points[0], points[1]);

            ui_canvas2D.DrawCanvas.DeselectAll(hDC);
            ui_canvas2D.DrawCanvas.SelectEntity(hDC, selectedEntity);
            ui_canvas2D.DrawCanvas.ReleaseHdc(hDC);
        }

		class nodeTag
		{
			public int id { get; set; }
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
						if (kvPair[0] == "AMP_U")
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

        // 제네릭 파일 조회해서 트리만드는 함수
        void addNode(string[] lines, ref TreeNode parentNode, ref int index)
        {
            while (index < lines.Length)
            {
                string data = lines[index];
                index++;
                if(!data.Contains('='))
                {                    
                    if (data.StartsWith("END_OF_"))
                    {
                        break;
                    }
                    else
                    {
                        data = data.Replace("START_OF_", "");
                        data = data.Replace("_DATA", "");
                        TreeNode childNode = new TreeNode(data);
                        addNode(lines, ref childNode, ref index);
                        parentNode.Nodes.Add(childNode);
                    }
                }
                else
                {
                    TreeNode childNode = new TreeNode(data);
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

        // CONTOUR 자식 노드의 index로 entity의 시작점과 끝점을 구해서 list에 담아 리턴하는 함수
        List<double> findContourEntityByIndex(TreeNode parentNode, int index)
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

        // STRING 노드의 px, py 구하는 함수
        List<double> findStringEntity(TreeNode parentNode)
        {
            List<double> tnList = new List<double>();
            foreach (TreeNode node in parentNode.Nodes)
            {
                if (node.Text.StartsWith("STRING_POSITION_"))
                {
                    tnList.Add(double.Parse(node.Text.Split('=')[1]));
                }
            }
            return tnList;            
        }

        

        // list 받아서 그림그리는 함수
        void drawLines(List<Dictionary<string, double>> points)
        {
            double u = points[0]["START_U"];
            double v = points[0]["START_V"];
			int i = 1;
			while (i < points.Count)
			{
				if (points[i]["RADIUS"] == 0)
				{
					ui_canvas2D.DrawCanvas.AddLine(u, v, points[i]["U"], points[i]["V"]); 
				}
				else
				{
					if (points[i]["AMP"] < 0)
					{
						ui_canvas2D.DrawCanvas.AddArc(points[i]["ORIGIN_U"], points[i]["ORIGIN_V"], -points[i]["RADIUS"], 
							u, v, points[i]["U"], points[i]["V"]);
					}
					else
					{
						ui_canvas2D.DrawCanvas.AddArc(points[i]["ORIGIN_U"], points[i]["ORIGIN_V"], points[i]["RADIUS"], 
							u, v, points[i]["U"], points[i]["V"]);
					}
				}
				u = points[i]["U"];
				v = points[i]["V"];
				i++;
			}
        }

        // string데이터 삽입
        void writeString(Dictionary<string, string> properties)
        {
            ui_canvas2D.DrawCanvas.AddText(double.Parse(properties["STRING_POSITION_U"]), double.Parse(properties["STRING_POSITION_V"]),
			double.Parse(properties["STRING_HEIGHT"]), double.Parse(properties["STRING_ANGLE"]), 0, properties["STRING"]);            
        }

        #region Event
        #endregion
    }
}
