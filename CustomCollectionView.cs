
namespace CustomCollectionViewTester
{
    public interface ITreeviewItem
    {
        string Title { get; set; }

    }
    internal class CustomCollectionView<T> : CollectionView where T : ITreeviewItem
    {


        #region *// Bindable properties


        #region *// Item source
        public static readonly BindableProperty CustomItemsSourceProperty = BindableProperty.Create(nameof(CustomItemsSource), typeof(List<T>), typeof(CustomCollectionView<T>), null);

        public List<T> CustomItemsSource
        {
            get { return (List<T>)GetValue(CustomItemsSourceProperty); }
            set { SetValue(CustomItemsSourceProperty, value); }
        }

        #endregion


        #region *// InvalidateCommand
        public static BindableProperty InvalidateCommandProperty = BindableProperty.Create(nameof(InvalidateCommand), typeof(Action), typeof(CustomCollectionView<T>), null, BindingMode.OneWayToSource);

        public Action InvalidateCommand
        {
            get { return (Action)GetValue(InvalidateCommandProperty); }
            set { SetValue(InvalidateCommandProperty, value); }
        }
        #endregion


        #region *// Selected item

        public static readonly BindableProperty CustomSelectedItemProperty = BindableProperty.Create(nameof(CustomSelectedItem), typeof(ITreeviewItem), typeof(CustomCollectionView<T>), null);

        public ITreeviewItem CustomSelectedItem
        {
            get { return (ITreeviewItem)GetValue(CustomSelectedItemProperty); }
            set { SetValue(CustomSelectedItemProperty, value); }
        }

        #endregion 

        #endregion

        public class CollectionViewItem
        {
            public string Title { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                if (obj is not CollectionViewItem) return false;
                return ((CollectionViewItem)this).Title.Equals(((CollectionViewItem)obj).Title);
            }
            public override int GetHashCode()
            {
                return ((CollectionViewItem)this).Title.GetHashCode();
            }
        }
        private List<CollectionViewItem> CollectionViewItems = new();

        public CustomCollectionView()
        {
            ItemTemplate = new DataTemplate(() => {

                var label = new Label ();
                label.SetBinding(Label.TextProperty, nameof(CollectionViewItem.Title));

                var grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.Add(label, 0, 0);
                return grid;
            });

            SelectionMode = SelectionMode.Single;

            InvalidateCommand = new Action(() =>
            {
                Invalidate();
            });
        }

        private void Invalidate()
        {
            CollectionViewItems.Clear();
            foreach (var itemSource in CustomItemsSource)
            {
                CollectionViewItems.Add(new CollectionViewItem { Title = itemSource.Title });
            };


            ItemsSource = CollectionViewItems;
            if (CustomSelectedItem != null)
            {
                var selectedCollectionViewItem = CollectionViewItems.FirstOrDefault(o => o.Title == CustomSelectedItem.Title);
                ScrollTo(selectedCollectionViewItem, null, ScrollToPosition.Center, false);
                SelectedItem = selectedCollectionViewItem;
            }
        }
    }
}
