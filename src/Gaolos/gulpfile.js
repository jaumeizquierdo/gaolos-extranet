/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

'use strict';

var gulp = require('gulp');
var sass = require('gulp-sass');

// compresión sass > css

gulp.task('sass', function () {
    return gulp.src('wwwroot/sass/**/*.scss')
      .pipe(sass().on('error', sass.logError))
      .pipe(gulp.dest('wwwroot/css'));
});

gulp.task('bootstrap', function () {
    return gulp.src('wwwroot/lib/bootstrap/scss/**/*.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(gulp.dest('wwwroot/dist/css/bootstrap.css'));
});

// watch extranet sass

gulp.task('sass:watch', function () {
    gulp.watch('wwwroot/sass/**/*.scss', ['sass']);
});

gulp.task('bootstrap:watch', function () {
    gulp.watch('wwwroot/lib/bootstrap/scss/**/*.scss', ['bootstrap']);
});

gulp.task('default', function () {
    // place code for your default task here
});

gulp.task('default', ['sass', 'bootstrap'])