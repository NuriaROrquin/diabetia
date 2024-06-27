import Link from "next/link";
import Switch from '@mui/material/Switch';

export const Input = ({type, placeholder, id, width, icon, withForgotPassword=false, defaultValue = null}) => {
  return (
      <>
          <div className={`${type !== "password" && "mb-4"} ${width} flex items-center relative`}>
              {icon && (
                  <div className="absolute mr-2 w-10 md:text-gray-secondary pl-4">
                      {icon}
                  </div>
              )}
              <input
                  type={type}
                  id={id}
                  placeholder={placeholder}
                  defaultValue={defaultValue}
                  className={`border border-gray-400 rounded-lg p-3 ${icon ? "pl-14" : ""}  w-full focus:outline-none text-gray-primary`}/>
          </div>

          {withForgotPassword && (
              <Link href="/auth/password-recover" className="mt-1 md:text-gray-secondary text-sm underline self-end" >Olvidé mi contraseña</Link>
          )}
      </>
  )
}

export const InputWithLabel = ({type, placeholder, label, id, width, icon, defaultValue = null, disabled = false, onChange}) => {
    return (
        <div className={`flex flex-col items-start text-base text-blue-primary font-medium gap-2 ${width}`}>
            <label htmlFor={id}>{label}</label>
            <div className="w-full flex relative items-center">
                {icon && (
                    <div className="absolute mr-2 w-10 text-gray-secondary pl-4">
                        {icon}
                    </div>
                )}
                <input
                    type={type}
                    id={id}
                    placeholder={placeholder}
                    defaultValue={defaultValue}
                    disabled={disabled}
                    onChange={onChange}
                    className={`border border-gray-400 rounded-lg p-3 ${icon ? "pl-14" : ""} w-full focus:outline-none text-gray-primary font-normal`}/>
            </div>
        </div>
    )
}

export const TextArea = ({label, id, width, placeholder, rows, defaultValue, onChange}) => {
    return (
        <div className={`flex flex-col items-start text-base text-blue-primary font-medium gap-2 ${width}`}>
            <label htmlFor={id}>{label}</label>
            <textarea
                id={id}
                className={`border border-gray-400 rounded-lg p-3 w-full focus:outline-none text-gray-primary font-normal`}
                placeholder={placeholder}
                rows={rows || 5}
                defaultValue={defaultValue}
                onChange={onChange}
            />
        </div>
    )
}

export const CustomSwitch = ({label, id, width, checked, onChange}) => {
    return (
        <div className={`flex flex-col items-start text-base text-blue-primary font-medium gap-2 ${width}`}>
            <label htmlFor={id}>{label}</label>
            <div className="w-full flex relative items-center">
                <Switch
                    checked={checked}
                    onChange={onChange}
                    inputProps={{ 'aria-label': 'controlled' }}
                />
            </div>
        </div>
    )
}