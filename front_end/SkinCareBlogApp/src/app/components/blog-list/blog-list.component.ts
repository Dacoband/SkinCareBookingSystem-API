import { BlogService } from './../../services/blog.service';
import { Component, OnInit } from '@angular/core';
import { Blog } from '../../models/blog.model';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-blog-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './blog-list.component.html',
  styleUrl: './blog-list.component.css'
})
export class BlogListComponent implements OnInit {
  blogs: Blog[] | null = null;

  constructor(private blogService: BlogService, private router: Router) { }

  ngOnInit(): void {
      this.blogService.getAllBlogs().subscribe({
        next: (blogs) => this.blogs =blogs,
        error: (error) => console.error('Error fetching blogs:', error)
      });
  }
  navigateToDetail(id: string): void {
    this.router.navigate(['/blog-detail', id]);
  }
}
