import { Link as RouterLink } from "react-router-dom"
import { PropsWithChildren } from "react"
import styled from "styled-components"

type LinkProps = PropsWithChildren<{
    href: string,
    onClick?: () => void
}>

const StyledLink = styled(RouterLink)`
    display: inline-block;
    font-family: 'Staatliches', system-ui;
    color: #ffffff;
    
    text-decoration: none;
    font-size: 2rem;
    
    padding: 0.5rem 1rem;
    background-color: #ff69b4;
    border: 3px solid #000000;
    border-radius: 32px;
    box-shadow: 4px 4px 0px #000000;
    transition: all 0.125s ease-in-out;

    &:active {
        transform: translate(2px, 2px);
        box-shadow: 0px 0px 0px #000000;
        background-color: #ff1493;
    }

    @media screen and (min-width: 1200px) {
        font-size: 3rem;
        padding: 1rem 0;
        border-width: 8px;
        border-radius: 16px;
        box-shadow: 6px 6px 0px #000000;

        &:hover {
            transform: translate(-3px, -3px);
            box-shadow: 9px 9px 0px #000000;
            background-color: #ffb6c1;
        }

        &:active {
            transform: translate(3px, 3px);
            box-shadow: 0px 0px 0px #000000;
            background-color: #ff1493;
        }
    }
`

const Link = ({ href, children, onClick }: LinkProps) => {
    return (
        <StyledLink to={href} onClick={onClick}>
            {children}
        </StyledLink>
    )
}

export default Link;
