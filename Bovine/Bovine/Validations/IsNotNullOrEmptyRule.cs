using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Bovine.Validations
{
    public class IsNotNullOrEmptyRule : Behavior<Entry>
    {
        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(IsNotNullOrEmptyRule), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

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
            IsValid = !string.IsNullOrWhiteSpace(e.NewTextValue);
            //((Entry)sender).Placeholder = IsValid ?  string.Empty : "O identificador é obrigatório";
            //((Entry)sender).PlaceholderColor = IsValid ? Color.Default : Color.Red;
        }        
    }
}
