import React from "react";
import { Link } from "react-router-dom";
import { withTranslation } from 'react-i18next';
import { Trans, Plural, Select } from 'react-i18next/icu.macro';

import "./RegisterLicencePage.css";

class RegisterLicencePage extends React.Component {
    constructor() {
        super();

        this.state = { isLicenceAgreementChecked: false };

        this.licenceAgreementClick = this.licenceAgreementClick.bind(this);
    }

    licenceAgreementClick() {
        this.setState(prevState => ({
            isLicenceAgreementChecked: !prevState.isLicenceAgreementChecked
          }));
    }

    render() {
        return (
            <div className="registerLicence_container">
                <div className="registerLicence_text_container">
                    <pre className="registerLicence_text">
                        <p>Copyright (c) 2023 Developer</p>

                        <p>Permission is hereby granted, free of charge, to any person obtaining a copy
                        of this software and associated documentation files (the "Software"), to deal
                        in the Software without restriction, including without limitation the rights
                        to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
                        copies of the Software, and to permit persons to whom the Software is
                        furnished to do so, subject to the following conditions:</p>

                        <p>The above copyright notice and this permission notice shall be included in all
                        copies or substantial portions of the Software.</p>

                        <p>THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
                        IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
                        FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
                        AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
                        LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
                        OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
                        SOFTWARE.</p>
                    </pre>
                    <input type="checkbox" id="licence_agreement" onClick={this.licenceAgreementClick} />
                    <label for="licence_agreement"><Trans>I accept the agreement</Trans></label>
                </div>

                <div className="registerLicence_buttons">
                    <Link className="root_a_button" to="/">
                        <Trans>Cancel</Trans>
                    </Link>
                    {
                        this.state.isLicenceAgreementChecked ? (
                            <Link className="root_a_button" to="/register">
                                <Trans>Confirm</Trans>
                            </Link>
                        ) : (
                            <Link className="root_a_button root_a_button_disabled" to="/register">
                                <Trans>Confirm</Trans>
                            </Link>
                        )
                    }
                </div>
            </div>
        );
    }
}

export default withTranslation("translation") (RegisterLicencePage);