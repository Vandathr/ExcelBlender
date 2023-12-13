using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRDataExtractor
{
    internal class FilezSelectPanel: Panel
    {
        private readonly Visual refToVisual;

        private readonly OpenFileDialog FileOpener = new OpenFileDialog();
        private readonly SaveFileDialog FileSaver = new SaveFileDialog();
        private DialogResult ResultOfDialog;

        private readonly int defaultDistance = 20;
        private readonly Size StandardTextBoxSize = new Size(200, 50);
        private readonly Size StandardButtonSize = new Size(200, 30);

        private readonly TextBox[] pathBoxN = new TextBox[2];

        private readonly Button SelectFirstExcelButton = new Button();
        private readonly Button SelectSecondExcelButton = new Button();
        private readonly Button LoadFilezButton = new Button();





        public FilezSelectPanel(Visual refToVisual)
        {
            this.refToVisual = refToVisual;

            Size = new Size(500, 200);
            BorderStyle = BorderStyle.Fixed3D;

            FileOpener.Filter = "Excel(*.xls;*.xlsx)|*.xls;*.xlsx|All files(*.*)|*.*";
            FileSaver.Filter = "Excel(*.xls;*.xlsx)|*.xls;*.xlsx|All files(*.*)|*.*";

            

            var align = defaultDistance;
            var floor = defaultDistance;

            pathBoxN[0] = new TextBox();
            pathBoxN[0].Size = StandardTextBoxSize;
            pathBoxN[0].Location = new Point(align, floor);
            Controls.Add(pathBoxN[0]);

            align = pathBoxN[0].Right + defaultDistance;
            floor = defaultDistance;

            SelectFirstExcelButton.Size = StandardButtonSize;
            SelectFirstExcelButton.Location = new Point(align, floor);
            SelectFirstExcelButton.Text = "Выбрать Таблицу Родитель";

            SelectFirstExcelButton.Click += (sender, args) =>
            {
                ResultOfDialog = FileOpener.ShowDialog();

                if (ResultOfDialog != DialogResult.Cancel)
                {
                    pathBoxN[0].Text = FileOpener.FileName;
                }
            };

            Controls.Add(SelectFirstExcelButton);


            align = defaultDistance;
            floor = pathBoxN[0].Bottom + defaultDistance;

            pathBoxN[1] = new TextBox();
            pathBoxN[1].Size = StandardTextBoxSize;
            pathBoxN[1].Location = new Point(align, floor);
            Controls.Add(pathBoxN[1]);


            align = pathBoxN[1].Right + defaultDistance;
            floor = pathBoxN[0].Bottom + defaultDistance;

            SelectSecondExcelButton.Size = StandardButtonSize;
            SelectSecondExcelButton.Location = new Point(align, floor);
            SelectSecondExcelButton.Text = "Выбрать Таблицу Потомок";

            SelectSecondExcelButton.Click += (sender, args) =>
            {
                ResultOfDialog = FileOpener.ShowDialog();

                if (ResultOfDialog != DialogResult.Cancel)
                {
                    pathBoxN[1].Text = FileOpener.FileName;
                }
            };

            Controls.Add(SelectSecondExcelButton);

            align = defaultDistance;
            floor = pathBoxN[1].Bottom + defaultDistance;

            LoadFilezButton.Size = StandardButtonSize;
            LoadFilezButton.Location = new Point(align, floor);
            LoadFilezButton.Text = "Загрузить файлы";

            LoadFilezButton.Click += (sender, args) => refToVisual.LoadFilez(pathBoxN[0].Text, pathBoxN[1].Text);


            Controls.Add(LoadFilezButton);

        }


    }
}
