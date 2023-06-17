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
  input: Board | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<BoardAddComponent>,
    @Inject(MAT_DIALOG_DATA) data: Board,
    private todoApiService: TodoapiService
  ) {
    if (data) {
      this.input = data;
      const datetime = new Date(data.dueTime);
      this.date = datetime.toLocaleDateString(); // 9/17/2016
      this.time = datetime.toLocaleTimeString(); // 11:18:48 AM
      // cut seconds
      this.time = this.time.slice(0, -3);
      this.editForm = this.formBuilder.group({
        id: [data.id],
        name: [data.name, Validators.required],
        description: [data.description],
        dueDate: [datetime],
        dueTime: [this.time],
      });
    } else {
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
  }

  ngOnInit() {}

  save() {
    if (this.editForm.valid) {
      const hours = this.editForm.value.dueTime.split(':')[0];
      const minutes = this.editForm.value.dueTime.split(':')[1];
      console.log(hours);
      console.log(minutes);
      console.log(this.input?.dueTime);
      this.editForm.value.dueDate.setUTCHours(hours, minutes);

      console.log(new Date(this.editForm.value.dueDate));

      if (this.input) {
        const editedBoard: Board = {
          id: this.editForm.value.id,
          name: this.editForm.value.name,
          description: this.editForm.value.description,
          dueTime: new Date(this.editForm.value.dueDate.toString()),
        };
        this.todoApiService
          .updateBoard(editedBoard)
          .subscribe((item: Board) => {
            this.dialogRef.close(item);
          });
      } else {
        const newBoard: Board = {
          name: this.editForm.value.name,
          description: this.editForm.value.description,
          dueTime: new Date(this.editForm.value.dueDate.toString()),
        };
        console.log(newBoard);
        this.todoApiService.createBoard(newBoard).subscribe((item: Board) => {
          this.dialogRef.close(newBoard);
        });
      }
    }
  }

  close() {
    this.dialogRef.close();
  }
}
