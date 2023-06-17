import { Component, Inject, OnInit } from '@angular/core';
import {
  AbstractControl,
  AsyncValidatorFn,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable, delay, map } from 'rxjs';
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
      this.date = datetime.toLocaleDateString();
      this.time = datetime.toLocaleTimeString();
      this.time = this.time.slice(0, -3);
      this.editForm = this.formBuilder.group({
        id: [data.id],
        name: [data.name, Validators.required, this.minLengthValidator(3)],
        description: [data.description, Validators.required],
        dueDate: [datetime, Validators.required],
        dueTime: [this.time],
      });
    } else {
      const datetime = new Date();
      this.date = datetime.toLocaleDateString();
      this.time = datetime.toLocaleTimeString();
      // cut seconds
      this.time = this.time.slice(0, -3);
      this.editForm = this.formBuilder.group({
        id: [],
        name: [, Validators.required, this.minLengthValidator(3)],
        description: [, Validators.required],
        dueDate: [datetime, Validators.required],
        dueTime: [this.time],
      });
    }
  }

  ngOnInit() {}

  save() {
    if (this.editForm.valid) {
      const hours = this.editForm.value.dueTime.split(':')[0];
      const minutes = this.editForm.value.dueTime.split(':')[1];
      this.editForm.value.dueDate.setHours(hours, minutes);
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
        this.todoApiService.createBoard(newBoard).subscribe((item: Board) => {
          this.dialogRef.close(item);
        });
      }
    }
  }

  close() {
    this.dialogRef.close();
  }

  minLengthValidator(minLength: number): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      return new Observable<ValidationErrors | null>((observer) => {
        const value: string = control.value || '';
        if (value.length >= minLength) {
          observer.next(null);
        } else {
          observer.next({
            minLength: {
              requiredLength: minLength,
              actualLength: value.length,
            },
          });
        }
        observer.complete();
      });
    };
  }
}
