﻿#pragma checksum "..\..\..\Windows\RoomManagementWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C00C5489C0A5378306C7012F4C24B1E79DF0C2ED141810FC2CD7742ECD589D79"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using HospitalInformationSystem.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace HospitalInformationSystem.Windows {
    
    
    /// <summary>
    /// RoomManagementWindow
    /// </summary>
    public partial class RoomManagementWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Windows\RoomManagementWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton newRadioButton;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Windows\RoomManagementWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton deleteOneRoomRadioButton;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Windows\RoomManagementWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton deleteAllRoomsRadioButton;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Windows\RoomManagementWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton allRoomsRadioButton;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Windows\RoomManagementWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton oneRoomRadioButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HospitalInformationSystem;component/windows/roommanagementwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\RoomManagementWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.newRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 2:
            this.deleteOneRoomRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 3:
            this.deleteAllRoomsRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 4:
            this.allRoomsRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 5:
            this.oneRoomRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            
            #line 17 "..\..\..\Windows\RoomManagementWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.confirmButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

