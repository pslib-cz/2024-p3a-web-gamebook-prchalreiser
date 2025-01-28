import { Link as RouterLink } from "react-router-dom"
import { PropsWithChildren } from "react"
import styled from "styled-components"

type LinkProps = PropsWithChildren<{
    href: string,
    onClick?: () => void,
    className?: string
}>

const StyledLink = styled(RouterLink)`
    display: inline-flex;
    align-items: center;
    justify-content: center;
    font-family: 'Staatliches', system-ui;
    color: #ffffff;
    text-decoration: none;
    font-size: 1rem; // Reduced font size
    padding: 0.9rem 2rem;
    background: linear-gradient(135deg, #ff69b4 0%, #ff1493 100%);
    border: none;
    border-radius: 12px;
    position: relative;
    overflow: hidden;
    transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
    box-shadow: 0 4px 15px rgba(255, 105, 180, 0.3),
                0 2px 4px rgba(0, 0, 0, 0.1),
                inset 0 -2px 0 rgba(0, 0, 0, 0.2);
    
    &::before {
        content: '';
        position: absolute;
        top: 0;
        left: -100%;
        width: 100%;
        height: 100%;
        background: linear-gradient(
            120deg,
            transparent,
            rgba(255, 255, 255, 0.2),
            transparent
        );
        transition: 0.5s;
    }

    &:hover {
        transform: translateY(-2px);
        box-shadow: 0 8px 25px rgba(255, 105, 180, 0.4),
                    0 4px 8px rgba(0, 0, 0, 0.2),
                    inset 0 -2px 0 rgba(0, 0, 0, 0.3);
        background: linear-gradient(135deg, #ff83c3 0%, #ff1493 100%);
        
        &::before {
            left: 100%;
        }
    }

    &:active {
        transform: translateY(1px);
        box-shadow: 0 2px 8px rgba(255, 105, 180, 0.3),
                    0 1px 2px rgba(0, 0, 0, 0.2),
                    inset 0 2px 4px rgba(0, 0, 0, 0.1);
        background: linear-gradient(135deg, #ff1493 0%, #ff69b4 100%);
    }

    @media screen and (min-width: 1200px) {
        font-size: 2rem; // Reduced font size
        padding: 1.1rem 2.5rem;
    }

    @media (max-width: 768px) {
        width: 100%;
        font-size: 0.9rem; // Reduced font size
        padding: 0.8rem 1.5rem;
    }
`

const Link = ({ href, children, onClick, className }: LinkProps) => {
    return (
        <StyledLink to={href} onClick={onClick} className={className}>
            {children}
        </StyledLink>
    )
}

export default Link;
