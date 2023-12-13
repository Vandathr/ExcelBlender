using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRDataExtractor
{
    internal class Visual: Form
    {
        private readonly Model refToModel;

        private readonly FilezSelectPanel PanelOfFilezSelect;
        private readonly BlendingPanel PanelOfBlending;

        private readonly SaveFileDialog FileSaver = new SaveFileDialog();
        private DialogResult ResultOfDialog;

        private readonly int defaultDistance = 20;
        private readonly Size StandardButtonSize = new Size(200, 30);

        private readonly Button DoBlendingButton = new Button();


        public void LoadFilez(string pathToParentTable, string pathToChildTable) 
        { 
            refToModel.LoadFilez(pathToParentTable, pathToChildTable);
            Refresh();
        }

        public void BlendTablez() => refToModel.BlendTablez();

        public void Refresh()
        {
            PanelOfBlending.SetItemz(refToModel.GetParentTableColumnNameN(), refToModel.GetChildTableColumnNameN());
        }

        public void SetParentJoinColumnName(string columnName) => refToModel.SetParentJoinColumnName(columnName);
        public void SetChildJoinColumnName(string columnName) => refToModel.SetChildJoinColumnName(columnName);

        public void SetSelectedColumnz(string[] columnN) => refToModel.SetSelectedColumnz(columnN);

        public Visual(Model refToModel)
        {
            PanelOfFilezSelect = new FilezSelectPanel(this);
            PanelOfBlending = new BlendingPanel(this);

            this.refToModel = refToModel;

            FileSaver.Filter = "Excel(*.xls;*.xlsx)|*.xls;*.xlsx|All files(*.*)|*.*";

            Size = new Size(700, 600);

            var align = defaultDistance;
            var floor = defaultDistance;
            Controls.Add(PanelOfFilezSelect);

            align = defaultDistance;
            floor = PanelOfFilezSelect.Bottom;

            PanelOfBlending.Location = new Point(0, floor);
            Controls.Add(PanelOfBlending);

            align = defaultDistance;
            floor = PanelOfBlending.Bottom + defaultDistance;

            DoBlendingButton.Size = StandardButtonSize;
            DoBlendingButton.Location = new Point(align, floor);
            DoBlendingButton.Text = "Создать и сохранить Excel";

            DoBlendingButton.Click += (sender, args) =>
            {
                ResultOfDialog = FileSaver.ShowDialog();

                if (ResultOfDialog != DialogResult.Cancel)
                {
                    refToModel.SaveResult(FileSaver.FileName);
                }
            };

            Controls.Add(DoBlendingButton);


        }

    }
}
