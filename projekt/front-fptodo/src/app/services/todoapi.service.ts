import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import ToDoItem from '../models/todoitem';
import Board from '../models/board';
import User from '../models/user';
import ToDoItemDTO from '../models/todoItemDTO';

@Injectable({
  providedIn: 'root',
})
export class TodoapiService {
  private apiUrl = 'https://fptodo.azurewebsites.net/api/';
  // private apiUrl = 'http://localhost:5019/api/';

  private toDoItemsUrl = 'todoitems';
  private boardsUrl = 'boards';
  private userUrl = 'users';

  constructor(private http: HttpClient) {}

  // ToDoItem CRUD methods
  getAllToDoItems(): Observable<ToDoItem[]> {
    return this.http.get<ToDoItem[]>(`${this.apiUrl}${this.toDoItemsUrl}`);
  }

  getToDoItemById(id: number): Observable<ToDoItem> {
    const url = `${this.apiUrl}${this.toDoItemsUrl}/${id}`;
    return this.http.get<ToDoItem>(url);
  }

  createToDoItem(item: ToDoItemDTO): Observable<ToDoItem> {
    return this.http.post<ToDoItem>(`${this.apiUrl}${this.toDoItemsUrl}`, item);
  }

  updateToDoItem(item: ToDoItemDTO): Observable<ToDoItem> {
    const url = `${this.apiUrl}${this.toDoItemsUrl}/${item.id}`;
    return this.http.put<ToDoItem>(url, item);
  }

  deleteToDoItem(id: number): Observable<void> {
    const url = `${this.apiUrl}${this.toDoItemsUrl}/${id}`;
    return this.http.delete<void>(url);
  }

  // Board CRUD methods
  getAllBoards(): Observable<Board[]> {
    return this.http.get<Board[]>(`${this.apiUrl}${this.boardsUrl}`);
  }

  getBoardById(id: number): Observable<Board> {
    const url = `${this.apiUrl}${this.boardsUrl}/${id}`;
    return this.http.get<Board>(url);
  }

  getBoardItemsCountById(id: number): Observable<number> {
    const url = `${this.apiUrl}${this.boardsUrl}/${id}/countitems`;
    return this.http.get<number>(url);
  }

  getBoardPriorityById(id: number): Observable<number> {
    const url = `${this.apiUrl}${this.boardsUrl}/${id}/priority`;
    return this.http.get<number>(url);
  }

  createBoard(board: Board): Observable<Board> {
    return this.http.post<Board>(`${this.apiUrl}${this.boardsUrl}`, board);
  }

  updateBoard(board: Board): Observable<Board> {
    const url = `${this.apiUrl}${this.boardsUrl}/${board.id}`;
    return this.http.put<Board>(url, board);
  }

  deleteBoard(id: number): Observable<void> {
    const url = `${this.apiUrl}${this.boardsUrl}/${id}`;
    return this.http.delete<void>(url);
  }

  // User CRUD methods
  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}${this.userUrl}`);
  }

  getUserById(id: number): Observable<User> {
    const url = `${this.apiUrl}${this.userUrl}/${id}`;
    return this.http.get<User>(url);
  }

  createUser(user: User): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}${this.userUrl}`, user);
  }

  updateUser(user: User): Observable<User> {
    const url = `${this.apiUrl}${this.userUrl}/${user.id}`;
    return this.http.put<User>(url, user);
  }

  deleteUser(id: number): Observable<void> {
    const url = `${this.apiUrl}${this.userUrl}/${id}`;
    return this.http.delete<void>(url);
  }

  getAuthors(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}authors`);
  }
}
