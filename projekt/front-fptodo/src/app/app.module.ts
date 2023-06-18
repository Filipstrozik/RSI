import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { Component, Inject } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { NgIf } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { MatBadgeModule } from '@angular/material/badge';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { NgxMatTimepickerModule } from 'ngx-mat-timepicker';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { HomeComponent } from './components/home/home.component';
import { ToDoItemComponent } from './components/to-do-item/to-do-item.component';
import { BoardComponent } from './components/board/board.component';
import { UserComponent } from './components/users-management/user/user.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ToDoItemEditDialogComponent } from './components/to-do-item/to-do-item-edit-dialog/to-do-item-edit-dialog.component';
import { ToDoItemAddComponent } from './components/to-do-item/to-do-item-add/to-do-item-add.component';
import { BoardAddComponent } from './components/board/board-add/board-add.component';
import { UsersManagementComponent } from './components/users-management/users-management.component';
import { AddUserDialogComponent } from './components/users-management/add-user-dialog/add-user-dialog.component';
import { ToDoListComponent } from './components/to-do-list/to-do-list.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ToDoItemComponent,
    BoardComponent,
    UserComponent,
    ToDoItemEditDialogComponent,
    ToDoItemAddComponent,
    BoardAddComponent,
    UsersManagementComponent,
    AddUserDialogComponent,
    ToDoListComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatSlideToggleModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    HttpClientModule,
    MatCardModule,
    MatBadgeModule,
    MatCheckboxModule,
    FormsModule,
    MatDialogModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    NgxMatTimepickerModule,
    MatSnackBarModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
