import React from 'react'
//Higher order component
export default function withHover(Component, propName = 'hovering') {
    return class WithHover extends React.Component {
        state = {
            hovering : false
        }

        mouseOver = () => { this.setState({ hovering: true })}
    
        mouseOut = () => { this.setState({ hovering: false})}
        
        render () {            
            const props = {
                //Computed property names from es6
                //"...this.props" is called 'object-spread'
                [propName]: this.state.hovering,
                ...this.props
            }
            return (
                <div onMouseOver={this.mouseOver} onMouseOut={this.mouseOut}>                   
                    <Component {...props}/>
                </div>
            )
        }
    }
}