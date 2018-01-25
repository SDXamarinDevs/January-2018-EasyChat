using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

// https://www.grapecity.com/en/blogs/how-to-create-a-custom-color-picker-for-xamarin-forms/
namespace EasyChat.Controls
{
    public partial class ColorPicker : Grid
    {
        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create(nameof(Color), typeof(Color), typeof(ColorPicker), Color.Default, propertyChanged: OnColorPropertyChanged);

        private static void OnColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = bindable as ColorPicker;
            picker.bvColorPicker.Color = (Color)newValue;

        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public ColorPicker()
        {
            InitializeComponent();

            // creating the TapGestureRecognizer
            TapGestureRecognizer tapImgColorPicker = new TapGestureRecognizer();
            imgColorPicker.GestureRecognizers.Add(tapImgColorPicker);
            tapImgColorPicker.Tapped += TapImgColorPicker_Tapped;

            // set the image from embedded resource
            imgColorPicker.Source = ImageSource.FromResource("EasyChat.Controls.ColorPicker.png");

            // populate picker with available colors
            foreach (string colorName in colorDict.Keys)
            {
                pickerColorPicker.Items.Add(colorName);
            }
            bvColorPicker.Color = this.Color;
        }


        // Dictionary to get Color from color name.
        Dictionary<string, Color> colorDict = new Dictionary<string, Color>
        {
            { "Default", Color.Default },                  { "Amber", Color.FromHex("#FFC107") },
            { "Black", Color.FromHex("#212121") },         { "Blue", Color.FromHex("#2196F3") },
            { "Blue Grey", Color.FromHex("#607D8B") },     { "Brown", Color.FromHex("#795548") },
            { "Cyan", Color.FromHex("#00BCD4") },          { "Dark Orange", Color.FromHex("#FF5722") },
            { "Dark Purple", Color.FromHex("#673AB7") },   { "Green", Color.FromHex("#4CAF50") },
            { "Grey", Color.FromHex("#9E9E9E") },          { "Indigo", Color.FromHex("#3F51B5") },
            { "Light Blue", Color.FromHex("#02A8F3") },    { "Light Green", Color.FromHex("#8AC249") },
            { "Lime", Color.FromHex("#CDDC39") },          { "Orange", Color.FromHex("#FF9800") },
            { "Pink", Color.FromHex("#E91E63") },          { "Purple", Color.FromHex("#94499D") },
            { "Red", Color.FromHex("#D32F2F") },           { "Teal", Color.FromHex("#009587") },
            { "White", Color.FromHex("#FFFFFF") },         { "Yellow", Color.FromHex("#FFEB3B") },
        };

        private void TapImgColorPicker_Tapped(object sender, EventArgs e)
        {
            pickerColorPicker.Focus();
        }

        void pickerColorPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string colorName = pickerColorPicker.Items[pickerColorPicker.SelectedIndex];
            this.Color = colorDict[colorName];
        }
    }
}
