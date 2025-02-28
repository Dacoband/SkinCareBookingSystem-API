import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Blog } from '../models/blog.model';
@Injectable({
  providedIn: 'root'
})
export class BlogService {

  private apiUrl = 'https://localhost:7115/api/blog';
  constructor(private  http : HttpClient) { }
    getAllBlogs(): Observable<Blog[]> {
      return this.http.get<Blog[]>(this.apiUrl);  
    }
    getBlogById(id: string): Observable<Blog> {
      return this.http.get<Blog>(`${this.apiUrl}/${id}`);
    }
    createBlog(blog: Blog): Observable<number> {
      return this.http.post<number>(`${this.apiUrl}/create`, blog);
    }
    updateBlog(id: string, blog: Blog): Observable<number> {
      return this.http.put<number>(`${this.apiUrl}/${id}`, blog);
    }
    deleteBlog(id: string): Observable<boolean> {
      return this.http.delete<boolean>(`${this.apiUrl}/${id}`);
    }
}
