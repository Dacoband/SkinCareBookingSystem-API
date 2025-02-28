import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BlogService } from '../../services/blog.service';
import { Blog } from '../../models/blog.model';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
  selector: 'app-blog-delete',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    CommonModule,
    MatCardModule,
    MatFormFieldModule
  ],
  templateUrl: './blog-delete.component.html',
  styleUrls: ['./blog-delete.component.css']
})
export class BlogDeleteComponent implements OnInit {
  blog: Blog | null = null;
  errorMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private blogService: BlogService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.blogService.getBlogById(id).subscribe({
        next: (blog) => this.blog = blog,
        error: (err) => console.error('Error loading blog:', err)
      });
    }
  }

  onDelete(): void {
    if (this.blog) {
      this.blogService.deleteBlog(this.blog.id).subscribe({
        next: (result) => {
          if (result) {
            this.router.navigate(['/blog-list']);
          } else {
            this.errorMessage = 'Blog deletion failed.';
          }
        },
        error: (err) => this.errorMessage = 'Error deleting blog: ' + err.message
      });
    }
  }
}