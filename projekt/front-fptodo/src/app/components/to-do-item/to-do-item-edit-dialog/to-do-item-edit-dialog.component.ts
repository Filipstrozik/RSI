import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import Board from 'src/app/models/board';
import ToDoItemDTO from 'src/app/models/todoItemDTO';
import ToDoItem from 'src/app/models/todoitem';
import User from 'src/app/models/user';
import { TodoapiService } from 'src/app/services/todoapi.service';

@Component({
  selector: 'app-to-do-item-edit-dialog',
  templateUrl: './to-do-item-edit-dialog.component.html',
  styleUrls: ['./to-do-item-edit-dialog.component.css'],
})
export class ToDoItemEditDialogComponent implements OnInit {
  editForm: FormGroup;
  item: ToDoItem;
  public time = '';
  public date = '';

  users: User[] = [];

  boards: Board[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ToDoItemEditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data: ToDoItem,
    private todoApiService: TodoapiService
  ) {
    this.item = data;

    const datetime = new Date(this.item.dueTime);
    console.log(datetime);
    this.date = datetime.toLocaleDateString(); // 9/17/2016
    this.time = datetime.toLocaleTimeString(); // 11:18:48 AM
    // cut seconds
    this.time = this.time.slice(0, -3);
    console.log(this.time);
    this.editForm = this.formBuilder.group({
      id: [this.item.id],
      name: [this.item.name, Validators.required],
      isComplete: [this.item.isComplete],
      dueDate: [this.date],
      priority: [this.item.priority, Validators.required],
      boardId: [this.item.board.id, Validators.required],
      userId: [this.item.user?.id],
    });
  }

  ngOnInit() {
    this.todoApiService.getAllUsers().subscribe((users: User[]) => {
      this.users = users;
    });

    this.todoApiService.getAllBoards().subscribe((boards: Board[]) => {
      this.boards = boards;
    });
  }

  save() {
    if (this.editForm.valid) {
      const editedItem: ToDoItemDTO = this.editForm.value;

      console.log(this.date);
      console.log(this.time);

      editedItem.dueTime = new Date(
        `${this.editForm.value.dueDate} ${this.editForm.value.dueTime}`
      );

      console.log(editedItem);
      // Call your save method or emit the edited item to the parent component
      // Example: this.todoService.updateItem(editedItem);

      // this.todoApiService.updateToDoItem(editedItem).subscribe();
      this.dialogRef.close(editedItem);
    }
  }

  close() {
    this.dialogRef.close();
  }
}
