## Supported Selectors

| Selector | Example | Description |
| -------- | ------- | ----------- |
| .class | .header | Selects all elements with the StyleClass property containing 'header' |
| #id | #email | Selects all elements with StyleId set to email. If StyleId is not set, fallback to x:Name. When using Xaml, always prefer x:Name over StyleId. |
| * | * | Selects all elements |
| element | label | Selects all elements of type Label (but not subclasses). Case irrelevant. |
| ^base | ^contentpage | Selects all elements with ContentPage as base class, including ContentPage itself. Case irrelevant. This selector isn't present in the CSS specification, and only applies to XF. |
| element,element | label,button | Selects all Buttons and all Labels |
| element element | stacklayout label | Selects all Labels inside of a StackLayout |
| element>element | stacklayout>label | Selects all Labels with StackLayout as a direct parent |
| element+element | label+entry | Selects all Entries directly after a Label |
| element~element | label~entry | Selects all Entries preceded by a Label |

## Unsupported Selectors (for this version)

- `[attribute]` selectors
- `@media` or `@supports` selectors
- `:` or `::` selectors

## Selector combinations

Selectors can be combined without limitation, like in StackLayout > ContentView > label.email. But keep it sane !

## Precedence

Styles with matching selectors are all applied, on by one, in definition order. Styles defined on the item itself is always applied last.

This is the expected behavior in most cases, even if doesn't 100% match common CSS implementations.

Specificity, and specificity overrides (`!important`) are not supported. This is a known issue.

## Supported properties

| Property | Applies to | Values (string literals are `grayed`, while types are italic) | Example |
| -------- | ---------- | ------------------------------------------------------------- | ------- |
| `background-color` | `VisualElement` | color (see Colors) &#124; `initial` | `background-color: springgreen;` |
| `background-image` | `Page` | string &#124; `initial` | `background-image: bg.png;` |
| `border-color` | `Button`, `Frame` | color (see Colors) &#124; `initial` | `border-color: #9acd32;` |
| `border-width` | `Button` | double &#124; `initial` | border-width: .5; |
| `color` | `Button`, `DatePicker`, `Editor`, `Entry`, `Label`, `Picker`, `SearchBar`, `TimePicker` | color (see Colors) &#124; `initial` | `color: rgba(255, 0, 0, 0.3);` |
| `direction` | `VisualElement` | `ltr` &#124; `rtl` &#124; `inherit` &#124; `initial` | `direction: rtl;` |
| `font-family` | `Button`, `DatePicker`, `Editor`, `Entry`, `Label`, `Picker`, `SearchBar`, `TimePicker`, `Span` | string &#124; `initial` | `font-family: Consolas;` |
| `font-size` | `Button`, `DatePicker`, `Editor`, `Entry`, `Label`, `Picker`, `SearchBar`, `TimePicker`, `Span` | double &#124; namedsize (see NamedSize) &#124; `initial` | `font-size: 12;` |
| `font-style` | `Button`, `DatePicker`, `Editor`, `Entry`, `Label`, `Picker`, `SearchBar`, `TimePicker`, `Span` | `bold` &#124; `italic` &#124; `initial` | `font-style: bold;` |
| `height` | `VisualElement` | double &#124; `initial` | `min-height: 250;` |
| `margin` | `View` | thickness (see Thickness) &#124; `initial` | `margin: 6 12;` |
| `margin-left` | `View` | thickness (see Thickness) &#124; `initial` | `margin-left: 3;` |
| `margin-top` | `View` | thickness (see Thickness) &#124; `initial` | `margin-top: 2;` |
| `margin-right` | `View` | thickness (see Thickness) &#124; `initial` | `margin-right: 1;` |
| `margin-bottom` | `View` | thickness (see Thickness) &#124; `initial` | `margin-bottom: 6;` |
| `min-height` | `VisualElement` | double &#124; `initial` | `min-height: 50;` |
| `min-width` | `VisualElement` | double &#124; `initial` | `min-width: 112;` |
| `opacity` | `VisualElement` | double &#124; `initial` | `opacity: .3;` |
| `padding` | `Layout`, `Page` | thickness (see Thickness) &#124; `initial` | `padding: 6 12 12;` |
| `padding-left` | `Layout`, `Page` | double &#124; `initial` | `padding-left: 3;` |
| `padding-top` | `Layout`, `Page` | double &#124; `initial` | `padding-top: 4;` |
| `padding-right` | `Layout`, `Page` | double &#124; `initial`padding-right: 2;` |
| `padding-bottom` | `Layout`, `Page` | double &#124; `initial` | `padding-bottom: 6;` |
| `text-align` | `Entry`, `EntryCell`, `Label`, `SearchBar` | `left` &#124; `right` &#124; `center` &#124; `start` &#124; `end` &#124; `initial`. It is recommended to avoid left and right in non-ltr environments | `text-align: right;` |
| `visibility` | `VisualElement` | `true` &#124; `visible` &#124; `false` &#124; `hidden` &#124; `collapse` &#124; `initial` | `visibility: hidden;` |
| `width` | `VisualElement` | double &#124; `initial` | `min-width: 320;` |

## Unsupported Common Properties

- `all: initial`
- layout properties (box, or grid). FlexLayout is coming, and it'll be CSS stylable,
- shorthand properties, like `font`, `border`

## Colors

- one of the 140 X11 color (https://en.wikipedia.org/wiki/X11_color_names), which happens to match CSS Colors, UWP predefined colors and XF Colors. Case insensitive
- HEX: `#rgb`, `#argb`, `#rrggbb`, `#aarrggbb`
- RGB: `rgb(255,0,0)`, `rgb(100%,0%,0%)` => values in range 0-255 or 0%-100%
- RGBA: `rgba(255, 0, 0, 0.8)`, `rgba(100%, 0%, 0%, 0.8)` => opacity is 0.0-1.0
- HSL: `hsl(120, 100%, 50%)` => h is 0-360, s and l are 0%-100%
- HSLA: `hsla(120, 100%, 50%, .8)` => opacity is 0.0-1.0

## Thickness

One, two, three or four values, separated by white spaces.

- a single value indicates uniform thickness
- two values indicates (resp.) vertical and horizontal thickness
- three values indicates (resp.) top, horizontal (left and right) and bottom thickness
- when using four values, they are top, right, bottom, left

> IMPORTANT: This differs from Xaml thickness definitions, which are
>
> 1. separated by commas (`,`)
> 1. are in the form of `uniform`, `horizontal`, `vertical` or `left`, `top`, `right`, `bottom`

## NamedSize

One of the following value, case insensitive. Exact meaning depends of the platform and the control

- `default`
- `micro`
- `small`
- `medium`
- `large`

## Initial

`initial` is a valid value for all properties. It clears the value (resets to default) that was set from another Style.

## Additional remarks

- no inheritance supported, meaning no `inherit` value and that you can't set the font-size to a layout, and expect all the labels in that layout to inherit the value. The only exception is the `direction` property, which supports `inherit`, and that's the default value.
- element are matched by name, no support for xmlns

## Usage

### XAML (preferred)

```xml
<ContentPage x:Class="...">
  <ContentPage.Resources>
    <StyleSheet Source="appresources/style.css" />
  </ContentPage.Resources>
</ContentPage>
```

the `Source` argument takes an Uri relative to the current xaml control, or relative to the application root if it starts with a `/`. The `style.css` has to be an EmbeddedResource.

alternatively, you can inline your style in a `CDATA` Section

```xml
<ContentPage x:Class="...">
  <ContentPage.Resources>
    <StyleSheet>
<![CDATA[
^contentpage {
    background-color: orange;
    padding: 20;
}

stacklayout > * {
    margin: 3;
}
]]>
    </StyleSheet>
  </ContentPage.Resources>
</ContentPage>
```

do not abuse of that second syntax.

### in C#

From an embedded resource:

```cs
myPage.Resources.Add(StyleSheet.FromAssemblyResource(this.GetType().Assembly, "resource.id.of.the.css"));
```

or from a TextReader:

```cs
using (var reader = new StringReader(my_css_string))
    myPage.Resources.Add(StyleSheet.FromReader(reader));
```

## StyleSheet, XamlC and other potential optimizations

At this time, CSS StyleSheets are parsed and evaluated at runtime. That aren't compiled. Every time a StyleSheet is used, it's reparsed again. If parsing time is an issue, enabling caching is trivial, but comes at memory cost.

