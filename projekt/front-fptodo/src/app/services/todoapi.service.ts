import { HttpClient, HttpParams } from '@angular/common/http';
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
  // private apiUrl = 'https://fptodo.azurewebsites.net/api/';
  private apiUrl = 'https://localhost:7192/api/';

  private toDoItemsUrl = 'todoitems';
  private boardsUrl = 'boards';
  private userUrl = 'users';

  constructor(private http: HttpClient) {}

  // ToDoItem CRUD methods
  getAllToDoItems(): Observable<ToDoItem[]> {
    return this.http.get<ToDoItem[]>(`${this.apiUrl}${this.toDoItemsUrl}`);
  }

  filterToDoItems(
    name: string,
    isComplete: boolean,
    minPrority: number,
    maxPriority: number
  ): Observable<ToDoItem[]> {
    let params = new HttpParams();

    if (name) {
      params = params.set('name', name);
    }

    if (isComplete !== null) {
      params = params.set('isComplete', isComplete.toString());
    }

    if (minPrority) {
      params = params.set('minPriority', minPrority.toString());
    }

    if (maxPriority) {
      params = params.set('maxPriority', maxPriority.toString());
    }

    const url = `${this.apiUrl}${this.toDoItemsUrl}/search`;
    return this.http.get<ToDoItem[]>(url, { params: params });
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

  filterBoards(name: string, description: string): Observable<Board[]> {
    const url = `${this.apiUrl}${this.boardsUrl}/search`;
    return this.http.get<Board[]>(url, { params: { name, description } });
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

  filterUsers(
    name: string,
    email: string,
    minAge: number,
    maxAge: number
  ): Observable<User[]> {
    let params = new HttpParams();

    if (name) {
      params = params.set('name', name);
    }
    if (email) {
      params = params.set('email', email);
    }
    if (minAge) {
      params = params.set('minAge', minAge.toString());
    }
    if (maxAge) {
      params = params.set('maxAge', maxAge.toString());
    }

    const url = `${this.apiUrl}/${this.userUrl}/search`;
    return this.http.get<User[]>(url, { params });
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
