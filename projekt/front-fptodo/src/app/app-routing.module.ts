import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { UsersManagementComponent } from './components/users-management/users-management.component';
import { ToDoListComponent } from './components/to-do-list/to-do-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' }, // default route
  { path: 'home', component: HomeComponent },
  { path: 'users', component: UsersManagementComponent },
  { path: 'todos', component: ToDoListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
