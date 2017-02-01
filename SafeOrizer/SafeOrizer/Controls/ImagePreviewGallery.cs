// From: https://github.com/rasmuschristensen/XamarinFormsImageGallery

namespace SafeOrizer.Controls
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using Xamarin.Forms;

    /// <summary>
    /// A horizontal collection of Views that allows Binding and Selection of Items
    /// </summary>
    public class ImagePreviewGallery : ScrollView
    {
        private readonly StackLayout imageStack;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePreviewGallery"/> class.
        /// </summary>
        public ImagePreviewGallery()
        {
            this.Orientation = ScrollOrientation.Horizontal;

            this.imageStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            this.Content = this.imageStack;
        }

        /// <summary>
        /// Gets the bindable Children of the View
        /// </summary>
        public IList<View> Children => this.imageStack.Children;

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IList),
                typeof(ImagePreviewGallery),
                default(IList),
                BindingMode.TwoWay,
                propertyChanging: (bindableObject, oldValue, newValue) =>
                {
                    ((ImagePreviewGallery)bindableObject).ItemsSourceChanging();
                },
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    ((ImagePreviewGallery)bindableObject).ItemsSourceChanged(bindableObject, (IList)oldValue, (IList)newValue);
                });

        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        void ItemsSourceChanging()
        {
            if (this.ItemsSource == null)
                return;
        }

        void ItemsSourceChanged(BindableObject bindable, IList oldValue, IList newValue)
        {
            if (this.ItemsSource == null)
                return;

            if (newValue is INotifyCollectionChanged notifyCollection)
            {
                notifyCollection.CollectionChanged += (sender, args) =>
                {
                    if (args.NewItems != null)
                    {
                        foreach (var newItem in args.NewItems)
                        {
                            var view = (View)this.ItemTemplate.CreateContent();
                            if (view is BindableObject bindableObject)
                                bindableObject.BindingContext = newItem;
                            this.imageStack.Children.Add(view);
                        }
                    }
                    if (args.OldItems != null)
                    {
                        // not supported
                        this.imageStack.Children.RemoveAt(args.OldStartingIndex);
                    }
                };
            }
        }

        public DataTemplate ItemTemplate
        {
            get;
            set;
        }

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(
                nameof(SelectedItem),
                typeof(object),
                typeof(ImagePreviewGallery),
                null,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((ImagePreviewGallery)bindable).UpdateSelectedIndex();
                });

        public object SelectedItem
        {
            get => this.GetValue(SelectedItemProperty);
            set => this.SetValue(SelectedItemProperty, value);
        }

        void UpdateSelectedIndex()
        {
            if (this.SelectedItem == this.BindingContext)
            {
                return;
            }

            this.SelectedIndex = this.Children
                .Select(c => c.BindingContext)
                .ToList()
                .IndexOf(this.SelectedItem);
        }

        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(
                nameof(SelectedIndex),
                typeof(int),
                typeof(ImagePreviewGallery),
                0,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((ImagePreviewGallery)bindable).UpdateSelectedItem();
                });

        public int SelectedIndex
        {
            get => (int)this.GetValue(SelectedIndexProperty);
            set => this.SetValue(SelectedIndexProperty, value);
        }

        private void UpdateSelectedItem() => this.SelectedItem = this.SelectedIndex > -1 ?
            this.Children[this.SelectedIndex].BindingContext : null;
    }
}




// OLD

//using Xamarin.Forms;
//using System.Collections;
//using System.Collections.Specialized;
//using System.Collections.Generic;
//using System.Linq;

//namespace SafeOrizer.Views
//{
//    // http://rasmustc.com/blog/Image-Gallery-With-Xamarin-Forms/
//    public class ImageGallery : ScrollView
//    {
//        readonly StackLayout _imageStack;

//        public ImageGallery()
//        {
//            this.Orientation = ScrollOrientation.Horizontal;

//            _imageStack = new StackLayout
//            {
//                Orientation = StackOrientation.Horizontal
//            };

//            this.Content = _imageStack;
//        }

//        public IList<View> Children
//        {
//            get
//            {
//                return _imageStack.Children;
//            }
//        }


//        public static readonly BindableProperty ItemsSourceProperty =
//            BindableProperty.Create<ImageGallery, IList>(
//                view => view.ItemsSource,
//                default(IList),
//                BindingMode.TwoWay,
//                propertyChanging: (bindableObject, oldValue, newValue) => {
//                    ((ImageGallery)bindableObject).ItemsSourceChanging();
//                },
//                propertyChanged: (bindableObject, oldValue, newValue) => {
//                    ((ImageGallery)bindableObject).ItemsSourceChanged(bindableObject, oldValue, newValue);
//                }
//            );

//        public IList ItemsSource
//        {
//            get
//            {
//                return (IList)GetValue(ItemsSourceProperty);
//            }
//            set
//            {

//                SetValue(ItemsSourceProperty, value);
//            }
//        }

//        void ItemsSourceChanging()
//        {
//            if (ItemsSource == null)
//                return;
//        }

//        void ItemsSourceChanged(BindableObject bindable, IList oldValue, IList newValue)
//        {
//            if (ItemsSource == null)
//                return;

//            var notifyCollection = newValue as INotifyCollectionChanged;
//            if (notifyCollection != null)
//            {
//                notifyCollection.CollectionChanged += (sender, args) => {
//                    if (args.NewItems != null)
//                    {
//                        foreach (var newItem in args.NewItems)
//                        {

//                            var view = (View)ItemTemplate.CreateContent();
//                            var bindableObject = view as BindableObject;
//                            if (bindableObject != null)
//                                bindableObject.BindingContext = newItem;
//                            _imageStack.Children.Add(view);
//                        }
//                    }
//                    if (args.OldItems != null)
//                    {
//                        // not supported
//                        _imageStack.Children.RemoveAt(args.OldStartingIndex);
//                    }
//                };
//            }

//        }

//        public DataTemplate ItemTemplate
//        {
//            get;
//            set;
//        }

//        public static readonly BindableProperty SelectedItemProperty =
//            BindableProperty.Create<ImageGallery, object>(
//                view => view.SelectedItem,
//                null,
//                BindingMode.TwoWay,
//                propertyChanged: (bindable, oldValue, newValue) => {
//                    ((ImageGallery)bindable).UpdateSelectedIndex();
//                }
//            );

//        public object SelectedItem
//        {
//            get
//            {
//                return GetValue(SelectedItemProperty);
//            }
//            set
//            {
//                SetValue(SelectedItemProperty, value);
//            }
//        }

//        void UpdateSelectedIndex()
//        {
//            if (SelectedItem == BindingContext)
//                return;

//            SelectedIndex = Children
//                .Select(c => c.BindingContext)
//                .ToList()
//                .IndexOf(SelectedItem);

//        }

//        public static readonly BindableProperty SelectedIndexProperty =
//            BindableProperty.Create<ImageGallery, int>(
//                carousel => carousel.SelectedIndex,
//                0,
//                BindingMode.TwoWay,
//                propertyChanged: (bindable, oldValue, newValue) => {
//                    ((ImageGallery)bindable).UpdateSelectedItem();
//                }
//            );

//        public int SelectedIndex
//        {
//            get
//            {
//                return (int)GetValue(SelectedIndexProperty);
//            }
//            set
//            {
//                SetValue(SelectedIndexProperty, value);
//            }
//        }

//        void UpdateSelectedItem()
//        {
//            SelectedItem = SelectedIndex > -1 ? Children[SelectedIndex].BindingContext : null;
//        }
//    }
//}
