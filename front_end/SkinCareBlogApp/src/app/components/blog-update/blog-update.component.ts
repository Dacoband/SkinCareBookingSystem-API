import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BlogService } from '../../services/blog.service';
import { Blog, BlogImage } from '../../models/blog.model';

@Component({
  selector: 'app-blog-update',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './blog-update.component.html',
  styleUrls: ['./blog-update.component.css']
})
export class BlogUpdateComponent implements OnInit {
  blogForm: FormGroup;
  blog: Blog | null = null;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private blogService: BlogService,
    private router: Router
  ) {
    this.blogForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      status: ['', Validators.required],
      imageUrl: ['']
    });
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.blogService.getBlogById(id).subscribe({
        next: (blog) => {
          this.blog = blog;
          this.blogForm.patchValue({
            title: blog.title,
            description: blog.description,
            status: blog.status,
            imageUrl: blog.blogImage && blog.blogImage.length > 0 ? blog.blogImage[0].imageUrl : ''
          });
        },
        error: (err) => console.error('Error loading blog:', err)
      });
    }
  }

  onSubmit(): void {
    if (this.blog && this.blogForm.valid) {
      const updatedBlog: Blog = {
        ...this.blog,
        title: this.blogForm.get('title')?.value,
        description: this.blogForm.get('description')?.value,
        status: this.blogForm.get('status')?.value,
        blogImage: this.blogForm.get('imageUrl')?.value ? [{ imageUrl: this.blogForm.get('imageUrl')?.value } as BlogImage] : this.blog.blogImage,
        updatedAt: new Date()
      };

      this.blogService.updateBlog(this.blog.id, updatedBlog).subscribe({
        next: (result) => {
          if (result > 0) {
            this.router.navigate(['/blog-list']);
          } else {
            this.errorMessage = 'Blog update failed.';
          }
        },
        error: (err) => this.errorMessage = 'Error updating blog: ' + err.message
      });
    }
  }
}