import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { MainComponent } from './main.component';
import { AccountService } from './services/account.service';
import { AuthenticatedGuard } from './guards/authenticated.guard';
import { AuthInterceptor } from './interceptors/auth.intercecptor';

import { CodemirrorModule } from '@ctrl/ngx-codemirror';
import { LoginComponent } from './components/login/login.component';
import { SideMenuComponent } from './components/side-menu/side-menu.component';
import { PageListComponent } from './components/structure/page/page-list.component';
import { PageManageComponent } from './components/structure/page/page-manage.component';
import { PageVersionListComponent } from './components/structure/page/version/page-version-list.component';
import { ActivityHistoryComponent } from './components/activity-history/activity-history.component';
import { ThemeListComponent } from './components/structure/theme/theme-list.component';
import { SiteSettingsComponent } from './components/settings/site-settings.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LayoutListComponent } from './components/structure/layout-list.component';
import { LayoutManageComponent } from './components/structure/layout-manage.component';
import { MenuListComponent } from './components/structure/menu/menu-list.component';
import { MenuManageComponent } from './components/structure/menu/menu-manage-component';
import { MenuItemListComponent } from './components/structure/menu/menu-item-list.component';
import { ResourcesComponent } from './components/resources/resources.component';
import { ResourceListComponent } from './components/resources/resource-list.component';
import { HtmlEditorComponent } from './components/html-editor/html-editor.component';
import { DiffComponent } from './components/diff/diff.component';
import { BlogListComponent } from './components/blog/blog-list.component';
import { BlogManageComponent } from './components/blog/blog-manage.component';
import { PostListComponent } from './components/post/post-list.component';
import { PostManageComponent } from './components/post/post-manage.component';
import { ActiveStateIndicatorComponent } from './components/helpers/active-state-indicator.component';
import { AppPageComponent } from './components/app-page/app-page.component';
import { CallbackPipe } from './pipes/callback.pipe';
import { ProductListComponent } from './components/product-system/product/product-list.component';
import { ProductManageComponent } from './components/product-system/product/product-manage.component';
import { CategoryListComponent } from './components/product-system/category/category-list.component';
import { CategoryManageComponent } from './components/product-system/category/category-manage.component';

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: '', component: MainComponent, canActivate: [AuthenticatedGuard], children: [
      { path: '', component: DashboardComponent, data: { pageTitle: 'Dashboard' } },
      { path: 'layouts', component: LayoutListComponent, data: { pageTitle: 'Layouts' } },
      { path: 'layout', component: LayoutManageComponent, data: { pageTitle: 'Layout' } },
      { path: 'pages', component: PageListComponent, data: { pageTitle: 'Pages' } },
      { path: 'page', component: PageManageComponent, data: { pageTitle: 'Create New Page' } },
      { path: 'page/:id', component: PageManageComponent, data: { pageTitle: 'Manage Page' } },

      { path: 'menus', component: MenuListComponent, data: { pageTitle: 'Menus' } },
      { path: 'menu', component: MenuManageComponent, data: { pageTitle: 'Create New Menu' } },
      { path: 'menu/:menuId', component: MenuManageComponent, data: { pageTitle: 'Manage Menu' } },

      // Blog system routes.
      { path: 'blogs', component: BlogListComponent, data: { pageTitle: 'Blogs' } },
      { path: 'blog', component: BlogManageComponent, data: { pageTitle: 'Create New Blog' } },
      { path: 'blog/:blogId', component: BlogManageComponent, data: { pageTitle: 'Manage Blog' } },
      { path: 'blog/:blogId/post', component: PostManageComponent, data: { pageTitle: 'Create New Post' } },
      { path: 'blog/:blogId/post/:postId', component: PostManageComponent, data: { pageTitle: 'Manage Post' } },

      // Product system routes.
      { path: 'products', component: ProductListComponent, data: { pageTitle: 'Products' } },
      { path: 'product', component: ProductManageComponent, data: { pageTitle: 'Create New Product' } },
      { path: 'product/:productId', component: ProductManageComponent, data: { pageTitle: 'Manage Product' } },
      { path: 'product/categories', component: CategoryListComponent, data: { pageTitle: 'Product Categories' } },
      { path: 'product/category', component: CategoryManageComponent, data: { pageTitle: 'Create New Product Category' } },
      { path: 'product/category/:categoryId', component: CategoryManageComponent, data: { pageTitle: 'Manage Product Category' } },

      { path: 'resources', component: ResourcesComponent, data: { pageTitle: 'Resources' } },
      { path: 'themes', component: ThemeListComponent, data: { pageTitle: 'Themes' } },
      { path: 'activity', component: ActivityHistoryComponent, data: { pageTitle: 'Activity History' } },
      { path: 'settings', component: SiteSettingsComponent, data: { pageTitle: 'Settings' } },
      { path: 'diff/:pageId/:pageVersionId', component: DiffComponent, data: { pageTitle: 'Diff Comparison' } },
    ]
  }
];

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    AppPageComponent,
    LoginComponent,
    SideMenuComponent,
    PageListComponent,
    PageManageComponent,
    PageVersionListComponent,
    ActivityHistoryComponent,
    ThemeListComponent,
    SiteSettingsComponent,
    DashboardComponent,
    LayoutListComponent,
    LayoutManageComponent,
    MenuListComponent,
    MenuManageComponent,
    MenuItemListComponent,
    ResourcesComponent,
    ResourceListComponent,
    HtmlEditorComponent,
    DiffComponent,
    BlogListComponent,
    BlogManageComponent,
    PostListComponent,
    PostManageComponent,
    ActiveStateIndicatorComponent,
    CallbackPipe,
    ProductListComponent,
    ProductManageComponent,
    CategoryListComponent,
    CategoryManageComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: false } // <-- debugging purposes only
    ),
    CodemirrorModule
  ],
  providers: [
    AccountService,
    AuthenticatedGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    AuthInterceptor
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
