using System.Windows.Input;

namespace CustomCollectionViewTester
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    internal class StartupPageModel
    {


        #region *// Display data for custom control
        public class DisplayItem : ITreeviewItem
        {
            public string Title { get; set; }

        }

        public List<DisplayItem> DisplayItems { get; set; } = new();

        public DisplayItem SelectedDisplayItem { get; set; }

        public Action InvalidateTreeviewCommand { get; set; } 
        #endregion

        public class DataItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public List<DataItem> data = new();

        public StartupPageModel()
        {
            #region *// Generate test data
            for (int i = 0; i < 100; i++)
            {
                data.Add(new DataItem { Id = i, Name = $"Item {i}" });
            }
            #endregion
        }

        public ICommand OnAppearing => new Command(() =>
        {

            foreach (var dataItem in data)
            {
                DisplayItems.Add(new DisplayItem { Title = dataItem.Name });
            }

            SelectedDisplayItem = DisplayItems[5];

            InvalidateTreeviewCommand.Invoke();
        });

        public ICommand Scroll => new Command(() =>
        {
            SelectedDisplayItem = DisplayItems[50];
            InvalidateTreeviewCommand.Invoke();
        });

    }
}
