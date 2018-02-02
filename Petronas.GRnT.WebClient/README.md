# PETRONAS - AngularJS Project Structure
#####
#### Author: **Hieu Tran**
#### Contact Info: __hieu.tranquoc@petronas.com__ or hieutranagi47@gmail.com

#
### Installing dev tool:
We need to install: NodeJS and NPM first.
# Install/update NPM by:
`npm install -g npm`
### Installing gulp tool:
`npm install -g gulp`
### Clone the project:
`git clone https://<your-account-name>@bitbucket.org/jerrybk108/angularjs_structure.git`  
Then rename the clone project and change current directory into the project:
`cd <project name>`
### Using bower to install dependency packages for the project.
`bower install`
### Using Node package management to install test server, run project, and build the project
`npm install`
### Configure run mode:
Open `<project name>/app/main/services/configuration.js` file, change `AppVale.RunMode` to `Prod/Dev/Mock`,  
or keep it as default to run at first time or while waiting for back-end team start their API server.
### Run project:
`gulp`
### Rebuild sass by manually:
`gulp sass`
### Reinject dependency files (css & js)
`gulp inject`
### Build code for production
`gulp build`
#

## For timezone issue (Server side time is GMT+0000 as causal)
If server side adds time with GMT+0800, we have to change timezone on client side to GMT+0000

## To disable message: "Slow network is detected. Fallback font will be used while loading:"
Open new tab: `chrome://flags/#enable-webfonts-intervention-v2, then disable this.`  
[See more](https://stackoverflow.com/questions/40143098/why-does-this-slow-network-detected-log-appear-in-chrome)

# Implement [md5](https://github.com/gdi2290/angular-md5)
`bower install angular-md5 --save`
#
# Bower and NPM bypass proxy
##Bower:
open .bowerrc file then edit similar bellow code in proxy and https-proxy
```
{
    "directory": "bower_components",
    "scripts": {
        "postinstall": "gulp inject:dev"
    },
    "proxy": "http://username:password@170.38.23.20:8080",
    "https-proxy": "http://username:password@170.38.23.20:8080"
}
```
## NPM:
Open terminal and type:
```
- Add proxy
npm config set proxy http://username:password@170.38.23.20:8080
npm config set https-proxy http://username:password@170.38.23.20:8080
- Remove proxy configuration
npm config rm proxy
npm config rm https-proxy
```
### Note: if your username or password has special characters, please convert them to [URL encode](https://www.w3schools.com/tags/ref_urlencode.asp) standar

#### For example:
> password: P@assw0rd (special characters are '@' and 0 (zero))  
> Convert to: P%40assw%30rd with zero (0) => %30 and @ => %40
#
## Remove unuse packages in the project:
Run `npm prune` to unbuild modules not listed in package.json.
### To uninstall a global package,type:
`npm uninstall -g <package>`
#### To uninstall a package called jshint, you would type:
`npm uninstall -g jshint`
#
## Git ignore does not work:
```
git rm . -r --cached
git add .
git commit -m "fixed untracked files"
git push
```