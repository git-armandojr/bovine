using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Bovine.Validations
{
    public class TextValidator : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Regex reg = new Regex("[0-9]");
            bool isValid = reg.IsMatch(e.NewTextValue);
            ((Entry)sender).TextColor = isValid ? Color.Red : Color.Default;
            ((Entry)sender).BackgroundColor = isValid ? Color.FromHex("#FBC5D0") : Color.Default;
        }
    }
}
