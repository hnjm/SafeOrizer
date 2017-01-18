using System;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using System.IO;
using com.refractored.fab;
using SafeOrizer.Droid.Renderers;
using SafeOrizer.Controls;

[assembly: ExportRenderer(typeof(FloatingActionButtonView), typeof(FloatingActionButtonViewRenderer))]
namespace SafeOrizer.Droid.Renderers
{
    public class FloatingActionButtonViewRenderer : ViewRenderer<FloatingActionButtonView, FrameLayout>
    {
        private const int MARGIN_DIPS = 16;
        private const int FAB_HEIGHT_NORMAL = 56;
        private const int FAB_HEIGHT_MINI = 40;
        private const int FAB_FRAME_HEIGHT_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_NORMAL;
        private const int FAB_FRAME_WIDTH_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_NORMAL;
        private const int FAB_MINI_FRAME_HEIGHT_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_MINI;
        private const int FAB_MINI_FRAME_WIDTH_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_MINI;
        private readonly Android.Content.Context context;
        private readonly FloatingActionButton fab;

        public FloatingActionButtonViewRenderer()
        {
            this.context = Forms.Context;

            var d = this.context.Resources.DisplayMetrics.Density;
            var margin = (int)(MARGIN_DIPS * d); // margin in pixels

            this.fab = new FloatingActionButton(this.context);
            var lp = new FrameLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
            {
                Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal,
                LeftMargin = margin,
                TopMargin = margin,
                BottomMargin = margin,
                RightMargin = margin
            };

            this.fab.LayoutParameters = lp;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<FloatingActionButtonView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= HandlePropertyChanged;

            if (this.Element != null)
            {
                //UpdateContent ();
                this.Element.PropertyChanged += HandlePropertyChanged;
            }

            this.Element.Show = this.Show;
            this.Element.Hide = this.Hide;

            SetFabImage(this.Element.ImageName);
            SetFabSize(this.Element.Size);

            this.fab.ColorNormal = this.Element.ColorNormal.ToAndroid();
            this.fab.ColorPressed = this.Element.ColorPressed.ToAndroid();
            this.fab.ColorRipple = this.Element.ColorRipple.ToAndroid();
            this.fab.HasShadow = this.Element.HasShadow;
            this.fab.Click += this.Fab_Click;

            var frame = new FrameLayout(this.context);
            frame.RemoveAllViews();
            frame.AddView(this.fab);

            SetNativeControl(frame);
        }

        public void Show(bool animate = true) => this.fab.Show(animate);

        public void Hide(bool animate = true) => this.fab.Hide(animate);

        void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Content")
            {
                this.Tracker.UpdateLayout();
            }
            else if (e.PropertyName == FloatingActionButtonView.ColorNormalProperty.PropertyName)
            {
                this.fab.ColorNormal = this.Element.ColorNormal.ToAndroid();
            }
            else if (e.PropertyName == FloatingActionButtonView.ColorPressedProperty.PropertyName)
            {
                this.fab.ColorPressed = this.Element.ColorPressed.ToAndroid();
            }
            else if (e.PropertyName == FloatingActionButtonView.ColorRippleProperty.PropertyName)
            {
                this.fab.ColorRipple = this.Element.ColorRipple.ToAndroid();
            }
            else if (e.PropertyName == FloatingActionButtonView.ImageNameProperty.PropertyName)
            {
                SetFabImage(this.Element.ImageName);
            }
            else if (e.PropertyName == FloatingActionButtonView.SizeProperty.PropertyName)
            {
                SetFabSize(this.Element.Size);
            }
            else if (e.PropertyName == FloatingActionButtonView.HasShadowProperty.PropertyName)
            {
                this.fab.HasShadow = this.Element.HasShadow;
            }
        }

        void SetFabImage(string imageName)
        {
            if (!string.IsNullOrWhiteSpace(imageName))
            {
                try
                {
                    var drawableNameWithoutExtension = Path.GetFileNameWithoutExtension(imageName);
                    var resources = this.context.Resources;
                    var imageResourceName = resources.GetIdentifier(drawableNameWithoutExtension, "drawable", this.context.PackageName);
                    this.fab.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeResource(this.context.Resources, imageResourceName));
                }
                catch (Exception ex)
                {
                    throw new FileNotFoundException("There was no Android Drawable by that name.", ex);
                }
            }
        }

        void SetFabSize(FloatingActionButtonSize size)
        {
            if (size == FloatingActionButtonSize.Mini)
            {
                this.fab.Size = FabSize.Mini;
                this.Element.WidthRequest = FAB_MINI_FRAME_WIDTH_WITH_PADDING;
                this.Element.HeightRequest = FAB_MINI_FRAME_HEIGHT_WITH_PADDING;
            }
            else
            {
                this.fab.Size = FabSize.Normal;
                this.Element.WidthRequest = FAB_FRAME_WIDTH_WITH_PADDING;
                this.Element.HeightRequest = FAB_FRAME_HEIGHT_WITH_PADDING;
            }
        }

        void Fab_Click(object sender, EventArgs e)
        {
            var clicked = this.Element.Clicked;
            if (this.Element != null && clicked != null)
            {
                clicked(sender, e);
            }
        }
    }
}