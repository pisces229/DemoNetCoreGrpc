const path = require('path');
const fs = require('fs');
const Webpack = require('webpack');
const CopyPlugin = require('copy-webpack-plugin');
const HtmlWebPackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const ForkTsCheckerWebpackPlugin = require('fork-ts-checker-webpack-plugin');

module.exports = {
  mode: 'development',
  entry: {
    index: path.resolve(__dirname, './src/index.tsx'),
  },
  output: {
    path: path.resolve(__dirname, 'dist'),
    filename: 'static/js/[name].js',
  },
  devtool: 'source-map',
  resolve: {
    extensions: ['.js', '.jsx', '.ts', '.tsx'],
    alias: {
      '@': path.resolve(__dirname, 'src'),
    },
  },
  module: {
    rules: [
      {
        test: /.(ts|tsx)$/,
        exclude: /node_modules/,
        use: {
          loader: 'babel-loader',
          options: {
            presets: [
              '@babel/preset-env',
              ['@babel/preset-react', { runtime: 'automatic' }],
              '@babel/typescript', 
            ],
            plugins: ['@babel/plugin-transform-runtime'],
            cacheDirectory: true,
            cacheCompression: false,
          },
        },
      },
      {
        test: /\.css$/,
        exclude: /node_modules/,
        use: [
          MiniCssExtractPlugin.loader,
          'css-loader',
        ],
      },
    ],
  },
  plugins: [
    new CopyPlugin({
      patterns: [
        {
          from: path.resolve(__dirname, 'public', 'favicon.ico'),
          to: path.resolve(__dirname, 'dist', 'favicon.ico'),
        },
      ],
    }),
    new HtmlWebPackPlugin({
      // favicon: path.join(__dirname, 'public', 'favicon.ico'),
      template: path.join(__dirname, 'public', 'index.html'),
      filename: 'index.html',
    }),
    new MiniCssExtractPlugin({
      filename: 'static/css/[name].css',
    }),
    new ForkTsCheckerWebpackPlugin({
      async: true,
    }),
  ],
  watchOptions: {
    ignored: /node_modules/,
  },
  devServer: {
    port: 9900,
    https: {
      key: fs.readFileSync('c:/workspace/mkcert/localhost+2-key.pem'),
      cert: fs.readFileSync('c:/workspace/mkcert/localhost+2.pem'),
    },
    historyApiFallback: true,
    hot: true,
    open: true,
  },
};
