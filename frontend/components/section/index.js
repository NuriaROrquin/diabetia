export const Section = ({children, className}) => {
    return (
        <section className={`w-full min-h-screen ${className}`}>
            {children}
        </section>
    )
}