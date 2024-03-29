﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Omniscient.Foundation.Contrib.Silverlight;
using Omniscient.Foundation.ApplicationModel.Presentation;
using Microsoft.Windows.Controls;
using Omniscient.Foundation.Data;
using Omniscient.Foundation.ApplicationModel.Presentation.Navigation;
using System.Windows.Media.Imaging;

namespace Contrib.Silverlight.Client
{
    public partial class Page : UserControl
    {
        private MyController _controller;

        public Page()
        {
            InitializeComponent();

            _controller = new MyController(this.ContentPanel);
            Navigator.Controller = _controller;
            _controller.Navigator = Navigator.Navigator;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyEntity entity = new MyEntity();
            entity.Name = TextBoxNewEntity.Text;
            if (string.IsNullOrEmpty(entity.Name)) entity.Name = "No name";
            MyModel model = new MyModel(entity);
            _controller.OpenView(model);
        }
    }

    public class MyController : IViewController
    {
        private List<IView> _views;

        public MyController(StackPanel panel)
        {
            Panel = panel;
            _views = new List<IView>();
        }

        private StackPanel Panel { get; set; }

        public IViewNavigator Navigator { get; set; }

        #region IViewController Members

        public IView OpenView(IModel model)
        {
            foreach (IView view in _views)
            {
                if (view.Model == model)
                {
                    Navigator.NavigateTo(view);
                    return view;
                }
            }

            TextBlockView v = new TextBlockView((MyModel)model);
            _views.Add(v);
            Navigator.NavigateTo(v);
            Panel.Children.Add(v.Block);
            return v;
        }

        public IView CurrentView
        {
            get;
            set;
        }

        public event EventHandler CurrentViewChanged;

        public void Focus(IView view)
        {
            TextBlockView myView;

            if (CurrentView != null)
            {
                myView = (TextBlockView)CurrentView;
                myView.Block.Opacity = 0.7;
                myView.Block.FontSize = 12;
            }

            myView = (TextBlockView)view;
            myView.Block.Opacity = 1.0;
            myView.Block.FontSize = 24;
            CurrentView = view;
        }

        public bool CloseView(IView view)
        {
            _views.Remove(view);
            Panel.Children.Remove(((TextBlockView)view).Block);
            return true;
        }

        public bool CloseViewRange(IEnumerable<IView> views)
        {
            foreach (IView v in views) CloseView(v);
            return true;
        }

        #endregion
    }

    public class TextBlockView : IView
    {
        public TextBlockView(MyModel model) 
        {
            Model = model;
            Block = new TextBlock();
            
            UpdateView();
        }

        public TextBlock Block { get; set; }

        public IModel Model
        {
            get;
            set;
        }

        public void UpdateView()
        {
            Block.Text = ((MyModel)Model).Name;    
        }

        public string Title
        {
            get { return Model.Name; }
            set { }
        }

        public object Icon
        {
            get;
            set;
        }
    }

    public class MyModel : ModelSingleEntityBase<MyEntity>
    {
        public MyModel(MyEntity entity) : base(entity) { }

        public override bool HasEntity(Guid id)
        {
            return base.Entity.Id == id;
        }

        public override bool ContainsEntitiesThatNeedToBeSaved()
        {
            return false;
        }

        public override IEntity GetEntity(Guid id)
        {
            if (base.Entity.Id == id) return base.Entity;
            return null;
        }

        public override string Name 
        {
            get { return Entity.Name; }
        }
    }

    public class MyEntity : EntityBase
    {
        public string Name { get; set; }
    }

}
