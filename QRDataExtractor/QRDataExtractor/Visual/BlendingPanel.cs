using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace QRDataExtractor
{
    internal class BlendingPanel: Panel
    {
        private readonly Visual refToVisual;

        private readonly ComboBox ParentTableJoinColumnBox = new ComboBox();
        private readonly ComboBox ChildTableJoinColumnBox = new ComboBox();

        private readonly ListBox ParentTableColumnzBox = new ListBox();
        private readonly ListBox ChildTableColumnzBox = new ListBox();
        private readonly ListBox ResultTableColumnzBox = new ListBox();

        private readonly Button BlendButton = new Button();


        private readonly int defaultDistance = 5;
        private readonly Size DefaultBoxSize = new Size(200, 30);
        private readonly Size DefaultBigBoxSize = new Size(200, 200);

        public void SetItemz(string[] parentColumnN, string[] childColiumnN)
        {
            ParentTableJoinColumnBox.Items.Clear();
            ParentTableJoinColumnBox.Items.AddRange(parentColumnN);

            ChildTableJoinColumnBox.Items.Clear();
            ChildTableJoinColumnBox.Items.AddRange(childColiumnN);

            ParentTableColumnzBox.Items.Clear();
            ParentTableColumnzBox.Items.AddRange(parentColumnN);

            ChildTableColumnzBox.Items.Clear();
            ChildTableColumnzBox.Items.AddRange(childColiumnN);

        }


        public BlendingPanel(Visual refToVisual)
        {
            this.refToVisual = refToVisual;

            var scroll = new VScrollBar();

            AutoScroll = true;

            Size = new Size(500, 400);
            BorderStyle = BorderStyle.Fixed3D;

            var align = defaultDistance;
            var floor = defaultDistance;

            ParentTableJoinColumnBox.Location = new Point(align, floor);
            ParentTableJoinColumnBox.Size = DefaultBoxSize;
            Controls.Add(ParentTableJoinColumnBox);

            align = ParentTableJoinColumnBox.Right + defaultDistance;
            floor = defaultDistance;

            ChildTableJoinColumnBox.Location = new Point(align, floor);
            ChildTableJoinColumnBox.Size = DefaultBoxSize;
            Controls.Add(ChildTableJoinColumnBox);

            align = defaultDistance;
            floor = ParentTableJoinColumnBox.Bottom + defaultDistance;

            ParentTableColumnzBox.Location = new Point(align, floor);
            ParentTableColumnzBox.Size = DefaultBigBoxSize;
            ParentTableColumnzBox.HorizontalScrollbar = true;

            ParentTableColumnzBox.DoubleClick += (sender, args) => {
                if(ParentTableColumnzBox.SelectedItem is not null)
                ResultTableColumnzBox.Items.Add(ParentTableColumnzBox.SelectedItem); 
            };

            Controls.Add(ParentTableColumnzBox);

            align = defaultDistance;
            floor = ParentTableColumnzBox.Bottom + defaultDistance;

            ChildTableColumnzBox.Location = new Point(align, floor);
            ChildTableColumnzBox.Size = DefaultBigBoxSize;
            ChildTableColumnzBox.HorizontalScrollbar = true;

            ChildTableColumnzBox.DoubleClick += (sender, args) => {
                if (ChildTableColumnzBox.SelectedItem is not null) 
                    ResultTableColumnzBox.Items.Add(ChildTableColumnzBox.SelectedItem); 
            };

            Controls.Add(ChildTableColumnzBox);

            align = ChildTableColumnzBox.Right + defaultDistance;
            floor = ChildTableJoinColumnBox.Bottom + defaultDistance;

            ResultTableColumnzBox.Location = new Point(align, floor);
            ResultTableColumnzBox.Size = DefaultBigBoxSize;
            ResultTableColumnzBox.HorizontalScrollbar = true;

            ResultTableColumnzBox.DoubleClick += (sender, args) => { 
                if (ResultTableColumnzBox.SelectedItem is not null)
                    ResultTableColumnzBox.Items.Remove(ResultTableColumnzBox.SelectedItem); 
            };

            Controls.Add(ResultTableColumnzBox);

            align = defaultDistance;
            floor = ChildTableColumnzBox.Bottom + defaultDistance;

            BlendButton.Location = new Point(align, floor);
            BlendButton.Size = DefaultBoxSize;
            BlendButton.Text = "Соединить таблицы";
            BlendButton.Click += (sender, args) =>
            {
                refToVisual.SetParentJoinColumnName(ParentTableJoinColumnBox.Text);
                refToVisual.SetChildJoinColumnName(ChildTableJoinColumnBox.Text);
                refToVisual.SetSelectedColumnz(ResultTableColumnzBox.Items.OfType<string>().ToArray());
                refToVisual.BlendTablez();
            };


            Controls.Add(BlendButton);


        }

    }
}
