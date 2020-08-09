var gulp = require('gulp');
var sass = require('gulp-sass');
var merge = require('merge-stream');
var sassGlob = require('gulp-sass-glob');
var concat = require('gulp-concat');
var cleanCss = require('gulp-clean-css');

sass.compiler = require('node-sass');

gulp.task('compile-theme-sass', function () {
    return gulp.src('./pages/themes/**/*.scss')
        .pipe(sassGlob())
        .pipe(sass({ outputStyle: 'compressed', errLogToConsole: true }))
        .pipe(cleanCss())
        .pipe(gulp.dest('./pages/themes/.'));
});

gulp.task('sass:watch', function () {
    gulp.watch(['./pages/themes/**/*.scss'], gulp.series('sass'));
});

gulp.task('sass', gulp.series('compile-theme-sass'));
