using static CustomCollectionViewTester.StartupPageModel;

namespace CustomCollectionViewTester
{
    internal class StartupPage<T>: ContentPage where T: DisplayItem
    {
        CustomCollectionView<T> customCollectionView;
        StartupPageModel viewModel;
        public StartupPage(StartupPageModel viewModel)
        {
            this.viewModel = viewModel;
            BindingContext = viewModel;

            var scrollButton = new Button { Text = "Scroll" };
            scrollButton.Clicked += (s, e) => {
                viewModel.Scroll.Execute(null);
            };

            #region *// Treeview
            customCollectionView = new CustomCollectionView<T>
            {
                Margin = 10,
            };
            customCollectionView.SetBinding(CustomCollectionView<T>.CustomItemsSourceProperty, new Binding(nameof(StartupPageModel.DisplayItems), BindingMode.TwoWay, source: viewModel));
            customCollectionView.SetBinding(CustomCollectionView<T>.CustomSelectedItemProperty, new Binding(nameof(StartupPageModel.SelectedDisplayItem), BindingMode.TwoWay, source: viewModel));
            customCollectionView.SetBinding(CustomCollectionView<T>.InvalidateCommandProperty, new Binding(nameof(StartupPageModel.InvalidateTreeviewCommand), BindingMode.OneWayToSource, source: viewModel));
            #endregion



            var layoutGrid = new Grid();
            layoutGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            layoutGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            layoutGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            layoutGrid.Add(scrollButton, 0, 0);
            layoutGrid.Add(customCollectionView, 0, 1);

            Content = layoutGrid;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.OnAppearing.Execute(null);
        }
    }
}
