import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import User from 'src/app/models/user';
import { TodoapiService } from 'src/app/services/todoapi.service';

@Component({
  selector: 'app-add-user-dialog',
  templateUrl: './add-user-dialog.component.html',
  styleUrls: ['./add-user-dialog.component.css'],
})
export class AddUserDialogComponent {
  editForm: FormGroup;
  public time = '';
  public date = '';
  input: User | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<AddUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data: User,
    private todoApiService: TodoapiService,
    private snackBar: MatSnackBar
  ) {
    if (data) {
      this.input = data;
      this.editForm = this.formBuilder.group({
        id: [data.id],
        name: [
          data.name,
          {
            validators: [
              Validators.required,
              Validators.minLength(3),
              Validators.maxLength(50),
            ],
          },
        ],
        email: [
          data.email,
          { validators: [Validators.required, Validators.email] },
        ],
        age: [
          data.age,
          {
            validators: [
              Validators.required,
              Validators.min(1),
              Validators.max(5),
            ],
          },
        ],
      });
    } else {
      this.editForm = this.formBuilder.group({
        id: [],
        name: [
          ,
          {
            validators: [
              Validators.required,
              Validators.minLength(3),
              Validators.maxLength(50),
            ],
          },
        ],
        email: [, { validators: [Validators.required, Validators.email] }],
        age: [
          ,
          {
            validators: [
              Validators.required,
              Validators.min(1),
              Validators.max(100),
            ],
          },
        ],
      });
    }
  }

  ngOnInit() {}

  save() {
    if (this.editForm.valid) {
      if (this.input) {
        const editedUser: User = {
          id: this.editForm.value.id,
          name: this.editForm.value.name,
          email: this.editForm.value.email,
          age: this.editForm.value.age,
        };
        this.todoApiService.updateUser(editedUser).subscribe((item: User) => {
          this.dialogRef.close(item);
        });
      } else {
        const newUser: User = {
          name: this.editForm.value.name,
          email: this.editForm.value.email,
          age: this.editForm.value.age,
        };
        this.todoApiService.createUser(newUser).subscribe(
          (item: User) => {
            this.dialogRef.close(item);
          },
          (error) => {
            this.dialogRef.close();
            this.snackBar.open(error.error, 'Close', {
              duration: 5000,
            });
          }
        );
      }
    }
  }

  close() {
    this.dialogRef.close();
  }
}
