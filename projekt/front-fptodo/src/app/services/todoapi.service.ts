import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import ToDoItem from '../models/todoitem';

@Injectable({
  providedIn: 'root'
})
export class TodoapiService {

  private apiUrl = 'https://fptodo.azurewebsites.net/api/';

  private toDoItemsUrl = 'todoitems';
  private boardsUrl = 'boards';
  private userUrl = 'users';
  constructor(private http: HttpClient) { }
  
  getAllToDoItems(): Observable<ToDoItem[]> {
    return this.http.get<ToDoItem[]>(this.apiUrl + this.toDoItemsUrl);
  }

  getToDoItemById(id: number): Observable<ToDoItem> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<ToDoItem>(url);
  }

  createToDoItem(item: ToDoItem): Observable<ToDoItem> {
    return this.http.post<ToDoItem>(this.apiUrl, item);
  }

  updateToDoItem(item: ToDoItem): Observable<ToDoItem> {
    const url = `${this.apiUrl}/${item.id}`;
    return this.http.put<ToDoItem>(url, item);
  }

  deleteToDoItem(id: number): Observable<void> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<void>(url);
  }







}
