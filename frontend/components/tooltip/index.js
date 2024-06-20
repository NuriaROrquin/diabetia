import {styled, Tooltip, tooltipClasses} from "@mui/material";

const CustomTooltip = styled(({ className, ...props }) => (
    <Tooltip {...props} classes={{ popper: className }} />
))(({ theme }) => ({
    [`& .${tooltipClasses.tooltip}`]: {
        backgroundColor: '#989898',
        color: '#fff',
        maxWidth: 220,
        fontSize: theme.typography.pxToRem(18),
        borderRadius: '20px',
        padding: '16px',
        textAlign: "center",
    },
    [`& .${tooltipClasses.arrow}`]: {
        color: '#808080',
    },
}));

export default CustomTooltip;