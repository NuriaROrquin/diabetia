import {styled, Tooltip, tooltipClasses} from "@mui/material";

const CustomTooltip = styled(({ className, ...props }) => (
    <Tooltip {...props} classes={{ popper: className }} />
))(({ theme }) => ({
    [`& .${tooltipClasses.tooltip}`]: {
        backgroundColor: '#333',
        color: '#fff',
        maxWidth: 220,
        fontSize: theme.typography.pxToRem(12),
        borderRadius: '20px',
        padding: '16px',
        textAlign: "center",
    },
    [`& .${tooltipClasses.arrow}`]: {
        color: theme.palette.common.black
    },
}));

export default CustomTooltip;