import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
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
    private todoApiService: TodoapiService
  ) {
    if (data) {
      this.input = data;
      this.editForm = this.formBuilder.group({
        id: [data.id],
        name: [data.name, Validators.required],
        email: [data.email, Validators.required],
        age: [data.age, Validators.required],
      });
    } else {
      this.editForm = this.formBuilder.group({
        id: [],
        name: [, Validators.required],
        email: [, Validators.required],
        age: [, Validators.required],
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
        this.todoApiService.createUser(newUser).subscribe((item: User) => {
          this.dialogRef.close(item);
        });
      }
    }
  }

  close() {
    this.dialogRef.close();
  }
}
