/*
 * Created Date: Monday, August 28th 2017, 9:48:43 am
 * Author: Hieu Tran
 * https://github.com/petebacondarwin/dgeni-example
 * https://toddmotto.com/documenting-angular-dgeni
 * Copyright (c) 2017 Petronas
 */

var path = require('canonical-path');
var packagePath = __dirname;
var Package = require('dgeni').Package;

module.exports = new Package('HTDoc', [
    require('dgeni-packages/ngdoc'),
    require('dgeni-packages/nunjucks')
])

.processor(require('./processors/index-page'))

// Let's add our API and Guide processors
.processor(require('./processors/guide-data'))

.processor(require('./processors/api-data'))

.config(function(log, readFilesProcessor, writeFilesProcessor) {
    // Set the log level to 'info', switch to 'debug' when troubleshooting
    log.level = 'info';

    readFilesProcessor.basePath = path.resolve(packagePath, '../..');

    readFilesProcessor.sourceFiles = [{
            include: 'app/**/**/*.js',
            basePath: 'app'
        },
        {
            include: 'docs/content/**/*.md',
            basePath: 'docs/content',
            fileReader: 'ngdocFileReader'
        }
    ];

    writeFilesProcessor.outputFolder = 'docs/build';
})

.config(function(templateFinder) {
    templateFinder.templateFolders.unshift(path.resolve(packagePath, 'templates'))
})

.config(function(computePathsProcessor, computeIdsProcessor) {
    computePathsProcessor.pathTemplates.push({
        docTypes: ['module'],
        pathTemplate: '${area}/${name}',
        outputPathTemplate: 'partials/${area}/${name}.html'
    });

    computePathsProcessor.pathTemplates.push({
        docTypes: ['componentGroup'],
        pathTemplate: '${area}/${moduleName}/${groupType}',
        outputPathTemplate: 'partials/${area}/${moduleName}/${groupType}.html'
    });

    computeIdsProcessor.idTemplates.push({
        docTypes: ['content', 'indexPage'],
        getId: function(doc) {
            return doc.fileInfo.baseName;
        },
        getAliases: function(doc) {
            return [doc.id];
        }
    });

    computePathsProcessor.pathTemplates.push({
        docTypes: ['content'],
        getPath: function(doc) {
            var docPath = path.dirname(doc.fileInfo.relativePath);
            if (doc.fileInfo.baseName !== 'index') {
                docPath = path.join(docPath, doc.fileInfo.baseName);
            }
            return docPath;
        },
        outputPathTemplate: 'partials/${path}.html'
    });

});