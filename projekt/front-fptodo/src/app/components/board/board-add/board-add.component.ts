import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import Board from 'src/app/models/board';
import ToDoItemDTO from 'src/app/models/todoItemDTO';
import ToDoItem from 'src/app/models/todoitem';
import User from 'src/app/models/user';
import { TodoapiService } from 'src/app/services/todoapi.service';

@Component({
  selector: 'app-board-add',
  templateUrl: './board-add.component.html',
  styleUrls: ['./board-add.component.css'],
})
export class BoardAddComponent {
  editForm: FormGroup;
  public time = '';
  public date = '';

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<BoardAddComponent>,
    @Inject(MAT_DIALOG_DATA) data: Board,
    private todoApiService: TodoapiService
  ) {
    const datetime = new Date();
    this.date = datetime.toLocaleDateString(); // 9/17/2016
    this.time = datetime.toLocaleTimeString(); // 11:18:48 AM
    // cut seconds
    this.time = this.time.slice(0, -3);
    this.editForm = this.formBuilder.group({
      id: [],
      name: [, Validators.required],
      description: [],
      dueDate: [datetime],
      dueTime: [this.time],
    });
  }

  ngOnInit() {}

  save() {
    if (this.editForm.valid) {
      const hours = this.editForm.value.dueTime.split(':')[0];
      const minutes = this.editForm.value.dueTime.split(':')[1];
      const newBoard: Board = {
        name: this.editForm.value.name,
        description: this.editForm.value.description,
        dueTime: new Date(this.editForm.value.dueDate.setHours(hours, minutes)),
      };
      this.todoApiService.createBoard(newBoard).subscribe((item: Board) => {
        this.dialogRef.close(item);
      });
    }
  }

  close() {
    this.dialogRef.close();
  }
}
