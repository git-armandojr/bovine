//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("Bovine.Pages.CattleDetailPage.xaml", "Pages/CattleDetailPage.xaml", typeof(global::Bovine.Pages.CattleDetailPage))]

namespace Bovine.Pages {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Pages\\CattleDetailPage.xaml")]
    public partial class CattleDetailPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label labelID;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Entry entryID;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label labelIdentifier;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Entry entryIdentifier;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Bovine.Validations.IsNotNullOrEmptyRule identifierValidator;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label labelErrorIdentifier;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label labelType;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label labelBirthDate;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Picker pickerSpecie;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.DatePicker datePickerBirthdate;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label labelSex;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label labelWeight;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Picker pickerSex;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Entry entryWeight;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Frame frameJson;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label labelJson;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button buttonJson;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button buttonSave;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button buttonDelete;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button buttonCancel;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(CattleDetailPage));
            labelID = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "labelID");
            entryID = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Entry>(this, "entryID");
            labelIdentifier = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "labelIdentifier");
            entryIdentifier = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Entry>(this, "entryIdentifier");
            identifierValidator = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Bovine.Validations.IsNotNullOrEmptyRule>(this, "identifierValidator");
            labelErrorIdentifier = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "labelErrorIdentifier");
            labelType = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "labelType");
            labelBirthDate = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "labelBirthDate");
            pickerSpecie = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Picker>(this, "pickerSpecie");
            datePickerBirthdate = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.DatePicker>(this, "datePickerBirthdate");
            labelSex = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "labelSex");
            labelWeight = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "labelWeight");
            pickerSex = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Picker>(this, "pickerSex");
            entryWeight = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Entry>(this, "entryWeight");
            frameJson = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Frame>(this, "frameJson");
            labelJson = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "labelJson");
            buttonJson = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "buttonJson");
            buttonSave = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "buttonSave");
            buttonDelete = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "buttonDelete");
            buttonCancel = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "buttonCancel");
        }
    }
}
