import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { BlogService } from '../../services/blog.service';
import { Blog, BlogImage } from '../../models/blog.model';

@Component({
  selector: 'app-blog-create',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule
  ],
  templateUrl: './blog-create.component.html',
  styleUrls: ['./blog-create.component.css']
})
export class BlogCreateComponent {
  blogForm: FormGroup;
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private blogService: BlogService, private router: Router) {
    this.blogForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      status: ['', Validators.required],
      imageUrl: ['']
    });
  }

  onSubmit(): void {
    if (this.blogForm.valid) {
      const blog: Blog = {
        id: '',
        title: this.blogForm.get('title')?.value,
        description: this.blogForm.get('description')?.value,
        status: this.blogForm.get('status')?.value,
        blogImage: this.blogForm.get('imageUrl')?.value ? [{ imageUrl: this.blogForm.get('imageUrl')?.value } as BlogImage] : []
      };

      this.blogService.createBlog(blog).subscribe({
        next: (result) => {
          if (result > 0) {
            this.router.navigate(['/blog-list']);
          } else {
            this.errorMessage = 'Blog creation failed.';
          }
        },
        error: (err) => this.errorMessage = 'Error creating blog: ' + err.message
      });
    }
  }
}