var _ = require('lodash');

function buildDocData(doc, extraData) {

    var splitName = doc.name.split('.');
    doc.stateName = _.camelCase(splitName);

    return _.assign({
        name: doc.name,
        stateName: doc.stateName,
        type: doc.docType,
        outputPath: doc.outputPath,
        url: doc.path,
    }, extraData);
}

module.exports = function apiPagesProcessor(moduleMap) {

    // Defining when the processor will run, and it's process
    return {
        $runAfter: ['paths-computed'],
        $runBefore: ['rendering-docs'],
        $process: process
    };

    // Our process method definition
    // Getting all docs as a parameter
    function process(docs) {

        var apiPages = _(docs)

        // Filtering our all the docs that are not in a module
        // and the ones that are componentGroups
        .filter(function(doc) {
            return doc.docType !== 'componentGroup';
        })

        // Filtering and grouping by Module
        .filter('module')
            .groupBy('module')

        // Map of our Module Docs
        .map(function(moduleDocs, moduleName) {

            var moduleDoc = _.find(docs, {
                docType: 'module',
                name: moduleName
            });

            // Making sure we don't get any exceptions when the module is undefined
            if (!moduleDoc) {
                return;
            }

            // Calling back to our generic method to build the object
            return buildDocData(moduleDoc, {
                docs: moduleDocs

                    .filter(function(doc) {
                    return doc.docType !== 'module';
                })

                    .map(buildDocData)
            });

        })

        // Removing null items
        .filter()

        // Get the value
        .value();

        // After all the processing is done, we push the changes to docs
        // Note here that we are using our constant template defined earlier
        // Name and Items are parsed with the template
        docs.push({
            name: 'API_DATA',
            template: 'constant.data.template.js',
            outputPath: 'src/api-data.js',
            items: apiPages
        });
    }
};