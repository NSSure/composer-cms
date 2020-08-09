var gulp = require('gulp');
var sass = require('gulp-sass');
var merge = require('merge-stream');
var sassGlob = require('gulp-sass-glob');
var concat = require('gulp-concat');
var cleanCss = require('gulp-clean-css');

sass.compiler = require('node-sass');

// Sass gulp overview
// a. Editing sass files in different class libraries
//     1. Place your sass file into a folder called styles
//     2. Run the command "gulp sass:watch" from the cmd or the task runner explorer
//     3. Make your changes to your sass file and save the changes
//     4. The "gulp sass:watch" command will run the "sass" command on file save
//     5. The "sass" command then executes a series of tasks in the following order 'copy-style-source', 'compile-layout-sass'.
//     6. 'copy-style-source' copies the sass source code to the output directory in the plugins folder for your class library
//     7. 'copy-layout-sass' compiles the layout sass with imports your libraries sass files and creates a final css output.

// source: './wwwroot/composer-cms/themes//styles/**/*.scss',
var folders = [{
    source: './wwwroot/composer-cms/themes/default/theme.scss',
    destination: './wwwroot/css/bundle.css'
}];

//gulp.task('copy-style-source', function () {
//    var tasks = folders.map(function (folder) {
//        return gulp.src(folder.source).pipe(gulp.dest(folder.destination));
//    });

//    return merge(tasks);
//});

//// You shouldn't need to use this if you are compiling your sass into one of the 7 main style sass files in plugins/db/layout/css.
//gulp.task('compile-project-sass', function () {
//    var tasks = folders.map(function (folder) {
//        return gulp.src(folder.source)
//            .pipe(concat('default.min.css'))
//            .pipe(sassGlob())
//            .pipe(sass({ outputStyle: 'compressed', errLogToConsole: true }))
//            .pipe(cleanCss())
//            .pipe(gulp.dest(folder.destination));
//    });

//    return merge(tasks);
//});

gulp.task('compile-theme-sass', function () {
    return gulp.src('./wwwroot/composer-cms/themes/default/theme.scss')
        .pipe(sassGlob())
        .pipe(sass({ outputStyle: 'compressed', errLogToConsole: true }))
        .pipe(cleanCss())
        .pipe(gulp.dest('./wwwroot/composer-cms/css'));
});

gulp.task('sass:watch', function () {
    gulp.watch(['./wwwroot/composer-cms/themes/default/theme.scss'], gulp.series('sass'));
});

//gulp.task('sass', gulp.series('copy-style-source', 'compile-layout-sass'));
gulp.task('sass', gulp.series('compile-theme-sass'));
