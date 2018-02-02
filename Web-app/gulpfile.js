/*
 * @CreateTime: Dec 4, 2017 10:10 PM
 * @Author: Hieu Tran
 * @Contact: hieutranagi47@gmail.com
 * @Last Modified By: Hieu Tran
 * @Last Modified Time: Jan 5, 2018 2:09 PM
 * @Description: Modify Here, Please 
 */

var gulp         = require('gulp');
var log          = require('fancy-log');
var jshint       = require('gulp-jshint');
var jscs         = require('gulp-jscs');
var sass         = require('gulp-sass');
var scssComments = require('gulp-strip-css-comments');
var rename       = require('gulp-rename');
var concat       = require('gulp-concat');
var minify       = require('gulp-minify-css');
var autoprefixer = require('gulp-autoprefixer');
var sourcemaps   = require('gulp-sourcemaps');
var merge        = require('merge-stream');
var inject       = require('gulp-inject');
var streamQueue  = require('streamqueue');
var order        = require('gulp-order');
var fs           = require('fs');
var path         = require('path');
var url          = require('url');
var browserSync  = require('browser-sync').create();
var clean        = require('gulp-clean');
var gulpsync     = require('gulp-sync')(gulp);
var Dgeni        = require('dgeni');
//Js from bower
var wiredep        = require('wiredep').stream;
var mainBowerFiles = require('gulp-main-bower-files');
var uglify         = require('gulp-uglify');
var gulpFilter     = require('gulp-filter');
var bowerFiles     = require('main-bower-files'),
    es             = require('event-stream');
var flatten        = require('gulp-flatten');

var verson = Date.now();
var build = 'dist';

var jsFile = [
    '*.js',
    './app/main/*.js',
    './app/main/**/*.js',
    './app/src/*.js',
    './app/src/**/*.js',
    './app/utilities/*.js',
    './app/utilities/**/*.js'
];

var scssWatcher = [
    './app/assets/scss/*.scss',
    './app/assets/scss/**/*.scss',
    './app/partials/**/*.scss',
    './app/partials/*.scss',
    './app/utilities/*.scss',
    './app/utilities/**/*.scss',
    './app/src/*.scss',
    './app/src/**/*.scss'
];

//Check JavaScript code style
gulp.task('style', function() {
    // Return to use in another place because of jscs()
    return gulp.src(jsFile)
        .pipe(jshint())
        .pipe(jshint.reporter('jshint-stylish', {
            verbose: true
        }))
        .pipe(jscs());
});

// Copy html
gulp.task('html-prod', function() {
    return gulp.src([
        '!app/assets/**/*.html',
        'app/index.html',
        'app/**/*.html'
    ])
    .pipe(gulp.dest(build));
});

gulp.task('utilities-prod', function() {
    return gulp.src([
        'app/assets/*.*',
        'app/assets/**/*.*',
        '!app/assets/css/*.*',
        '!app/assets/css/**/*.*',
        '!app/assets/scss/*.*',
        '!app/assets/scss/**/*.*'
    ])
    .pipe(gulp.dest(build + '/assets'));
});

// Fonts
gulp.task('fonts-dev', function() {
    return gulp.src([
        'bower_components/**/fonts/*.+(eot|eof|svg|ttf|woff|woff2)',
        'bower_components/**/fonts/**/*.+(eot|eof|svg|ttf|woff|woff2)'
    ])
    .pipe(flatten())
    .pipe(gulp.dest('app/assets/fonts/'));
});

gulp.task('fonts-prod', function() {
    return gulp.src([
        'bower_components/**/fonts/*.+(eot|eof|svg|ttf|woff|woff2)',
        'bower_components/**/fonts/**/*.+(eot|eof|svg|ttf|woff|woff2)'
    ])
    .pipe(flatten())
    .pipe(gulp.dest(build + '/assets/fonts/'));
});

//Merge scss file then compile for dev mode
gulp.task('sass', function() {
    return streamQueue({objectMode: true},
        gulp.src([
            './app/assets/scss/utilities/_reset.scss',
            './app/assets/scss/utilities/_variable.scss',
            './app/assets/scss/utilities/_mixin.scss',
            './app/assets/scss/utilities/_fonts.scss',
            './app/assets/scss/utilities/_utilities.scss',
            './app/assets/scss/style.scss'
        ]),
        gulp.src([
            './app/assets/scss/**/*.scss',
            './app/assets/scss/*.scss',
            './app/partials/*.scss',
            './app/partials/**/*.scss',
            './app/utilities/**/*.scss',
            './app/utilities/*.scss',
            './app/src/**/*.scss',
            './app/src/*.scss'
        ])
    )
    .pipe(scssComments())
    .pipe(concat('main-' + verson + '.scss'))
    .pipe(sass.sync({sourceComments: 'map'}).on('error', sass.logError))
    .pipe(sourcemaps.init({loadMaps: true}))
    .pipe(autoprefixer({
        browsers: ['last 2 versions']
    }))
    .pipe(sourcemaps.write('./'))
    .pipe(gulp.dest('./app/assets/css'))
    .pipe(browserSync.stream());
});
//Merge scss file then compile and minify them for production mode.
gulp.task('sass-prod', function() {
    return streamQueue({objectMode: true},
        gulp.src([
            './app/assets/scss/utilities/_reset.scss',
            './app/assets/scss/utilities/_variable.scss',
            './app/assets/scss/utilities/_mixin.scss',
            './app/assets/scss/utilities/_fonts.scss',
            './app/assets/scss/utilities/_utilities.scss',
            './app/assets/scss/style.scss'
        ]),
        gulp.src([
            './app/assets/scss/**/*.scss',
            './app/assets/scss/*.scss',
            './app/partials/*.scss',
            './app/partials/**/*.scss',
            './app/utilities/**/*.scss',
            './app/utilities/*.scss',
            './app/src/**/*.scss',
            './app/src/*.scss'
        ])
    )
    .pipe(scssComments())
    .pipe(concat('main-' + verson + '.scss'))
    .pipe(sass.sync({sourceComments: 'map'}).on('error', sass.logError))
    .pipe(sourcemaps.init({loadMaps: true}))
    .pipe(autoprefixer({
        browsers: ['last 2 versions']
    }))
    .pipe(sourcemaps.write('./'))
    .pipe(gulp.dest(build + '/assets/css'))
    .pipe(rename({suffix: '.min'}))
    .pipe(minify())
    .pipe(gulp.dest(build + '/assets/css'));
});

//Merge js libraries.
gulp.task('bower-js', function() {
    // From bower dependencies.
    var options = {
        bowerJson: require('./bower.json'), // Where bower.json is?
        directory: './bower_components', // Where bower package is?
        ignorePath: ''
    };

    var filterJS = gulpFilter('**/*.js', {
        restore: true
    });

    return gulp.src(['./bower.json'])
        .pipe(wiredep(options))
        .pipe(mainBowerFiles())
        .pipe(filterJS)
        .pipe(concat('vendor-' + verson + '.js'))
        .pipe(uglify())
        .pipe(filterJS.restore)
        .pipe(gulp.dest('./app/libs'));
});

gulp.task('bower-js-prod', function() {
    // From bower dependencies.
    var options = {
        bowerJson: require('./bower.json'), // Where bower.json is?
        directory: './bower_components', // Where bower package is?
        ignorePath: ''
    };

    var filterJS = gulpFilter('**/*.js', {
        restore: true
    });

    return gulp.src(['./bower.json'])
        .pipe(wiredep(options))
        .pipe(mainBowerFiles())
        .pipe(filterJS)
        .pipe(concat('vendor-' + verson + '.js'))
        .pipe(uglify())
        .pipe(filterJS.restore)
        .pipe(gulp.dest(build + '/libs'));
});

//Merge css file for bower libs then create a new file in ./app/assets/css:
gulp.task('bower-css', function() {
    return gulp.src([
        './bower_components/**/*.css'
    ]).pipe(sourcemaps.init())
    .pipe(concat('libs-' + verson + '.min.css'))
    .pipe(autoprefixer())
    .pipe(sourcemaps.write())
    .pipe(minify())
    .pipe(gulp.dest('./app/assets/css'));
});
//Merge vendor from bower libraries then copy into production build
gulp.task('bower-css-prod', function() {
    return gulp.src([
        './bower_components/**/*.css'
    ]).pipe(sourcemaps.init())
    .pipe(concat('libs-' + verson + '.min.css'))
    .pipe(autoprefixer())
    .pipe(sourcemaps.write())
    .pipe(minify())
    .pipe(gulp.dest(build + '/assets/css'));
});

//Merge all project js files
gulp.task('merge-js-prod', function() {
    return gulp.src([
        './app/main/*.js',
        './app/main/**/*.js',
        './app/src/*.js',
        './app/src/**/*.js',
        './app/utilities/*.js',
        './app/utilities/**/*.js'
    ]).pipe(order([
        'app.module.js', 'app.router.js'
    ]))
    .pipe(concat('app-' + verson + '.js'))
    .pipe(uglify())
    .pipe(gulp.dest(build + '/libs'));
});

/**
 * Inject files into *.html in partial folder (not include its children folder).
 * https://github.com/klei/gulp-inject#optionsignorepath
 * Wiredep will read the 'bower.json' file and the 'bower_components' directory and will inject the correct paths at the level of the placeholders marked 'bower' in the index page
 */
gulp.task('inject', gulpsync.sync(['bower-css', 'bower-js']), function() {
    // From my files.
    var injectSrcJS = gulp.src([
        './app/main/*.js',
        './app/main/**/*.js',
        './app/src/*.js',
        './app/src/**/*.js',
        './app/utilities/*.js',
        './app/utilities/**/*.js'
    ]).pipe(order([
        'app.module.js', 'app.router.js'
    ]));

    var injectBowerJSSrc = gulp.src([
        './app/libs/*.js'
    ]);

    var injectBowerCSS = gulp.src([
        './app/assets/css/*.css'
    ]);

    var injectFontFarmily = gulp.src([
        './app/assets/fonts/**/*.css'
    ]);

    var injectSrcCSS = gulp.src([
        './app/assets/css/*.css'
    ]);

    var injectOptions = {
        ignorePath: '/app'
    };

    return gulp.src('./app/*.html')
        .pipe(inject(injectBowerJSSrc, {
            ignorePath: '/app',
            starttag: '<!-- inject:libs:{{ext}} -->'}))
        .pipe(inject(injectFontFarmily, {
            ignorePath: '/app',
            starttag: '<!-- inject:fonts:{{ext}} -->'}))
        .pipe(inject(injectSrcCSS, injectOptions))
        .pipe(inject(injectSrcJS, injectOptions))
        .pipe(gulp.dest('./app'));
});

gulp.task('inject-prod', function() {
    // From my files.
    var injectJS = gulp.src([
        build + '/libs/vendor-*.js'
    ]);

    var injectcss = gulp.src([
        build + '/assets/css/*.min.css'
    ]);

    var injectFontFarmily = gulp.src([
        build + '/assets/fonts/**/*.css'
    ]);

    //Remove production inject file
    var injectSrcJS = gulp.src([
        build + '/libs/app-*.js'
    ]);

    var injectOptions = {
        ignorePath: '/' + build
    };

    return gulp.src(build + '/*.html')
        .pipe(inject(injectJS, {
            ignorePath: '/' + build,
            starttag: '<!-- inject:libs:{{ext}} -->'}))
        .pipe(inject(injectFontFarmily, {
            ignorePath: '/' + build,
            starttag: '<!-- inject:fonts:{{ext}} -->'}))
        .pipe(inject(injectcss, injectOptions))
        .pipe(inject(injectSrcJS, injectOptions))
        .pipe(gulp.dest(build));
});

// Clean
gulp.task('clean-dev', function () {
    return gulp.src(['app/libs', 'app/assets/css'],
        {read: false}
    ).pipe(clean());
});

gulp.task('clean-prod', function() {
    return gulp.src(['dist'], {read: false})
        .pipe(clean());
});

gulp.task('default',
    gulpsync.sync([
        'clean-dev', 'clean-prod', 'style',
        'sass', 'fonts-dev',
        'inject'
    ]),
    function() {
        var defaultFile = 'index.html';
        var folder = path.resolve(__dirname, './app');
        browserSync.init({
            open: true,
            port: 8080,
            notify: false,
            minify: false,
            server: {
                baseDir: 'app',
                middleware: function(req, res, next) {
                    var fileName = url.parse(req.url);
                    fileName = fileName.href.split(fileName.search).join('');
                    var fileExists = fs.existsSync(folder + fileName);
                    if (!fileExists && fileName.indexOf('browser-sync-client') < 0) {
                        req.url = '/' + defaultFile;
                    }
                    return next();
                }
            }
        });
        // Watching SCSS files
        gulp.watch(scssWatcher, ['sass']);
        // Watching JS files
        gulp.watch(jsFile).on('change', browserSync.reload);
        // Watching HTML files
        var htmlFile = [
            './app/*.html',
            './app/**/*.html'
        ];
        gulp.watch(htmlFile).on('change', browserSync.reload);
    }
);

gulp.task('build',
    gulpsync.sync([
        'clean-prod', 'html-prod',
        'sass-prod', 'bower-css-prod',
        'bower-js-prod', 'merge-js-prod',
        'utilities-prod',
        'fonts-prod', 'inject-prod'
    ]),
    function() {
        log('Built successful!');
        return true;
    }
);

gulp.task('dgeni', function() {
    var dgeni = new Dgeni([require('./docs/config')]);
    return dgeni.generate();
});