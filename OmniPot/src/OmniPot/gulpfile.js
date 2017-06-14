/// <binding />
var gulp = require('gulp'),
    gp_clean = require('gulp-clean'),
    gp_concat = require('gulp-concat'),
    gp_less = require('gulp-less'),
    gp_sourcemaps = require('gulp-sourcemaps'),
    gp_typescript = require('gulp-typescript'),
    gp_uglify = require('gulp-uglify'),
    gp_minifyHtml = require("gulp-minify-html"),
    gp_cleanCSS = require('gulp-clean-css'),
    gp_imagemin = require('gulp-imagemin');

/// Define paths
var srcPaths = {
    app: ['ClientApp/app/main.ts', 'ClientApp/app/**/*.ts'],
    js: [
        'ClientApp/js/**/*.js',
        'node_modules/core-js/client/shim.min.js',
        'node_modules/jquery/dist/*.js',
        'node_modules/zone.js/dist/zone.js',
        'node_modules/reflect-metadata/Reflect.js',
        'node_modules/systemjs/dist/system.src.js',
        'node_modules/typescript/lib/typescript.js',
        'node_modules/ng2-bootstrap/bundles/ng2-bootstrap.min.js',
        'node_modules/moment/moment.js'
    ],
    images: [
        'ClientApp/app/images/**/*.png',
        'ClientApp/app/images/**/*.jpg',
        'ClientApp/app/images/**/*.gif',
        'ClientApp/app/images/**/*.jpeg'
    ],
    template: [
        'ClientApp/app/template/**/*.html'],
    bootstrap: [
        'node_modules/bootstrap/dist/**/*.*'],
    css: [
        'ClientApp/app/css/*.css'],
    js_angular: [
        'node_modules/@angular/**'
    ],
    js_rxjs: [
        'node_modules/rxjs/**'
    ]
};

var destPaths = {
    app: 'wwwroot/app/',
    js: 'wwwroot/js/',
    css: 'wwwroot/css/',
    images: 'wwwroot/images/',
    bootstrap: 'wwwroot/js/bootstrap/',
    template: 'wwwroot/template/',
    js_angular: 'wwwroot/js/@angular/',
    js_rxjs: 'wwwroot/js/rxjs/'
};

// Compile, minify and create sourcemaps all TypeScript files 
// and place them to wwwroot/app, together with their js.map files.
gulp.task('app', ['app_clean'], function () {
    return gulp.src(srcPaths.app)
        .pipe(gp_sourcemaps.init())
        .pipe(gp_typescript(require('./tsconfig.json').compilerOptions))
        .pipe(gp_uglify({ mangle: false }))
        .pipe(gp_sourcemaps.write('/'))
        .pipe(gulp.dest(destPaths.app));
});

// Delete wwwroot/app contents
gulp.task('app_clean', function () {
    return gulp.src(destPaths.app + "*", { read: false }).pipe(gp_clean({ force: true }));
});

// Delete wwwroot/js contents
gulp.task('js_clean', function () {
    return gulp.src(destPaths.js + "*", { read: false }).pipe(gp_clean({ force: true }));
});

// Delete wwwroot/template contents
gulp.task('template_clean', function () {
    return gulp.src(destPaths.template + "*", { read: false }).pipe(gp_clean({ force: true }));
});

// Delete wwwroot/css contents
gulp.task('css_clean', function () {
    return gulp.src(destPaths.css + "*", { read: false }).pipe(gp_clean({ force: true }));
});

// Delete wwwroot/template contents
gulp.task('clean_all', ['app_clean', 'js_clean', 'template_clean', 'css_clean']);

// Copy all JS files from external libraries to wwwroot/js
gulp.task('js', function () {
    gulp.src(srcPaths.js_angular).pipe(gulp.dest(destPaths.js_angular));
    gulp.src(srcPaths.bootstrap).pipe(gulp.dest(destPaths.bootstrap));
    gulp.src(srcPaths.js_rxjs).pipe(gulp.dest(destPaths.js_rxjs));
    return gulp.src(srcPaths.js).pipe(gulp.dest(destPaths.js));
});

// Copy all HTML files from external libraries to wwwroot/template
gulp.task('template', ['template_clean'], function () {
    return gulp.src(srcPaths.template)
        .pipe(gp_sourcemaps.init())
        .pipe(gp_minifyHtml())
        .pipe(gp_sourcemaps.write('/'))
        .pipe(gulp.dest(destPaths.template));
});

// Copy all HTML files from external libraries to wwwroot/css
gulp.task('minify-css', ['css_clean'] , function () {
    return gulp.src(srcPaths.css)
        .pipe(gp_sourcemaps.init())
        .pipe(gp_cleanCSS())
        .pipe(gp_sourcemaps.write('/'))
        .pipe(gulp.dest(destPaths.css));
});

gulp.task('minify-images', function (cb) {
    return gulp.src(srcPaths.images)
        .pipe(gp_imagemin([
            gp_imagemin.gifsicle({ interlaced: true }),
            gp_imagemin.jpegtran({ progressive: true }),
            gp_imagemin.optipng({ optimizationLevel: 5 }),
            gp_imagemin.svgo({ plugins: [{ removeViewBox: true }] })
        ]))
        .pipe(gulp.dest(destPaths.images));
});

// Watch specified files and define what to do upon file changes
gulp.task('watch', function () {
    gulp.watch([srcPaths.app, srcPaths.js], ['app', 'js', 'template', 'minify-css']);
});

// Define the default task so it will launch all other tasks
gulp.task('default', ['app', 'js', 'template', 'minify-css', 'minify-images']);
//gulp.task('default', ['js', 'template', 'minify-css', 'minify-images']);