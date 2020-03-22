const path = require('path')
const HtmlWebpackPlugin = require('html-webpack-plugin')

module.exports = {
    entry: './app/index.js',
    output: {
        path: path.resolve(__dirname, 'dist'),
        filename: 'index_bundle.js'
    },
    module:{
        rules: [
            {test: /\.(js)$/, use: 'babel-loader'},
                 //Converts JSX for browser and converts newer JS for older JS
            {test: /\.(css)$/, use: [ 'style-loader', 'css-loader' ]}
        ]
    },
    mode: 'development',
    plugins: [
        new HtmlWebpackPlugin({
            template: './app/index.html'
        })
    ]   
}