import React from 'react';
import ReactDOM from 'react-dom';

const Modal = ({ isVisible, closeModal, children }) => {
    if (!isVisible) return null;

    return ReactDOM.createPortal(
        <div className="fixed text-2xl inset-0 bg-gray-800 bg-opacity-50 flex justify-center items-center z-50">
            <div className="bg-white p-10 rounded-xl shadow-lg relative">
                {children}
                <button onClick={closeModal} className="absolute top-10 right-10 text-gray-500 hover:text-gray-700">
                    X
                </button>
            </div>
        </div>,
        document.body
    );
};

export default Modal;