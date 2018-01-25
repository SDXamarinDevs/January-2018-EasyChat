using System;
using Xamarin.Forms;

namespace EasyChat.Controls
{
    public abstract class DataTemplateSelector<T> : DataTemplateSelector
        where T : class
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (item)
            {
                case T itemT:
                    return OnSelectTemplate(itemT, container);
                default:
                    return new DataTemplate(typeof(ViewCell));
            }
        }

        protected abstract DataTemplate OnSelectTemplate(T item, BindableObject container);
    }
}
