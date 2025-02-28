import { Routes } from '@angular/router';
import { BlogListComponent } from './components/blog-list/blog-list.component';
import { BlogDetailComponent } from './components/blog-detail/blog-detail.component';
import { BlogCreateComponent } from './components/blog-create/blog-create.component';
import { BlogUpdateComponent } from './components/blog-update/blog-update.component';
import { BlogDeleteComponent } from './components/blog-delete/blog-delete.component';

export const routes: Routes = [
    { path: '', redirectTo: '/blog-list', pathMatch: 'full' },
    { path: 'blog-list', component: BlogListComponent },
    { path: 'blog-detail/:id', component: BlogDetailComponent },
    { path: 'blog-create', component: BlogCreateComponent },
    { path: 'blog-update/:id', component: BlogUpdateComponent },
    { path: 'blog-delete/:id', component: BlogDeleteComponent }
];
