import { Component } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import User from 'src/app/models/user';
import { TodoapiService } from 'src/app/services/todoapi.service';
import { AddUserDialogComponent } from '../users-management/add-user-dialog/add-user-dialog.component';
import ToDoItem from 'src/app/models/todoitem';

@Component({
  selector: 'app-to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.css'],
})
export class ToDoListComponent {
  toDoItems: ToDoItem[] = [];
  name: string = '';
  isComplete: boolean | null = null;
  minPriority: number | null = null;
  maxPriority: number | null = null;

  constructor(
    private dialog: MatDialog,
    private todoApiService: TodoapiService
  ) {}

  ngOnInit(): void {
    this.updateToDoItems();
  }

  updateToDoItems() {
    this.todoApiService.getAllToDoItems().subscribe((toDoItems: ToDoItem[]) => {
      this.toDoItems = toDoItems;
    });
  }

  onAddUser() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.data = null;
  }

  filterToDoItems() {
    this.todoApiService
      .filterToDoItems(
        this.name,
        this.isComplete!,
        this.minPriority!,
        this.maxPriority!
      )
      .subscribe((toDoItems: ToDoItem[]) => {
        this.toDoItems = toDoItems;
      });
  }

  clearFilters(): void {
    this.name = '';
    this.isComplete = null;
    this.minPriority = null;
    this.maxPriority = null;
    this.updateToDoItems();
  }
}
