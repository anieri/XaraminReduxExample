﻿using System;
using Xamarin.Forms;
using MapApp.Models;
using MapApp.Store;
using MapApp.Actions;
using System.Collections.ObjectModel;

namespace MapApp.ViewModels {
    public class TodoViewModel : BaseViewModel {
        public Command NewTodoCommand { get; private set; }

        public TodoViewModel() {
            App.Store.Subscribe<TodoState>(todo => this.Todos = todo.Todos);

            this.NewTodoCommand = new Command(this.AddTodo, () => !String.IsNullOrEmpty(this.NewText));
        }

        private void AddTodo() {
            App.Store.Dispatch(new AddTodoAction {
                Todo = new Todo {
                    Text = this.NewText,
                }
            });

            this.NewText = "";
        }

        private ObservableCollection<Todo> todos;
        public ObservableCollection<Todo> Todos {
            get { return this.todos; }
            set { this.SetProperty(ref this.todos, value); }
        }

        private String newText = "";
        public String NewText {
            get { return this.newText; }
            set { this.SetProperty(ref this.newText, value, onChanged: this.UpdateButton); }
        }

        private void UpdateButton() => this.NewTodoCommand.ChangeCanExecute();
    }
}
