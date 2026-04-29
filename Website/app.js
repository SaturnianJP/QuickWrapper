import { baseLayerLuminance, StandardLuminance } from 'https://unpkg.com/@fluentui/web-components';

const LISTING_URL = "{{ listingInfo.Url }}";

const PACKAGES = {
{{~ for package in packages ~}}
  "{{ package.Name }}": {
    name: "{{ package.Name }}",
    displayName: "{{ if package.DisplayName; package.DisplayName; end; }}",
    description: "{{ if package.Description; package.Description; end; }}",
    version: "{{ package.Version }}",
    author: {
      name: "{{ if package.Author.Name; package.Author.Name; end; }}",
      url: "{{ if package.Author.Url; package.Author.Url; end; }}",
    },
    dependencies: {
      {{~ for dependency in package.Dependencies ~}}
        "{{ dependency.Name }}": "{{ dependency.Version }}",
      {{~ end ~}}
    },
    keywords: [
      {{~ for keyword in package.Keywords ~}}
        "{{ keyword }}",
      {{~ end ~}}
    ],
    license: "{{ package.License }}",
    licensesUrl: "{{ package.LicensesUrl }}",
  },
{{~ end ~}}
};

const setTheme = () => {
  const isDarkTheme = () => window.matchMedia("(prefers-color-scheme: dark)").matches;
  if (isDarkTheme()) {
    baseLayerLuminance.setValueFor(document.documentElement, StandardLuminance.DarkMode);
  } else {
    baseLayerLuminance.setValueFor(document.documentElement, StandardLuminance.LightMode);
  }
}

// =======================
// i18n
// To add a new language: add an entry to I18N below and a <fluent-option>
// in index.html. Every key from the `en` block must exist in the new locale.
// =======================
const I18N = {
  en: {
    addToVcc: "Add to VCC",
    copy: "Copy",
    howToAddListing: "How to add a listing to your VCC",
    helpTitle: "How to add this listing to your VCC",
    helpIntro: "To add this listing to your VCC (VRChat Creator Companion), do the following",
    helpStep1: 'Open your VCC and go to Settings',
    helpStep2: 'Click the "Packages" tab',
    helpStep3: 'Click "Add Repository"',
    helpStep4: "In the field that appears - paste the url displayed below",
    helpStep5: 'Click "Add"',
    helpStep6: 'Check the repository info and click "I Understand"',
    helpStep7: "Go to any of your projects to see the packages from the newly added listing.",
    listingUrl: "Listing URL",
    docsLink: 'You can read more about Package Listings <a href="https://vcc.docs.vrchat.com" target="_blank">on the VCC docs</a>',
    searchPlaceholder: "Search packages...",
    colName: "Name",
    colType: "Type",
    downloadZip: "Download .ZIP",
    packageInfo: "Package Info",
    author: "Author",
    dependencies: "Dependencies",
    keywords: "Keywords",
    license: "License",
    learnMore: "Learn More",
    poweredBy: 'Powered by <a href="https://vcc.docs.vrchat.com" target="_blank">VPM</a>',
    builtWith: 'Built with <a href="https://claude.ai" target="_blank">Claude</a>',
  },
  ja: {
    addToVcc: "VCCに追加",
    copy: "コピー",
    howToAddListing: "リスティングをVCCに追加する方法",
    helpTitle: "このリスティングをVCCに追加する方法",
    helpIntro: "このリスティングをVCC(VRChat Creator Companion)に追加するには、以下の手順を実行してください",
    helpStep1: "VCCを開き、設定画面に移動します",
    helpStep2: "「Packages」タブをクリックします",
    helpStep3: "「Add Repository」をクリックします",
    helpStep4: "表示されたフィールドに、下記のURLを貼り付けます",
    helpStep5: "「Add」をクリックします",
    helpStep6: "リポジトリ情報を確認し、「I Understand」をクリックします",
    helpStep7: "プロジェクトを開くと、追加されたリスティングのパッケージが利用できるようになります。",
    listingUrl: "リスティングURL",
    docsLink: 'パッケージリスティングの詳細は<a href="https://vcc.docs.vrchat.com" target="_blank">VCCドキュメント</a>をご参照ください',
    searchPlaceholder: "パッケージを検索...",
    colName: "名前",
    colType: "種類",
    downloadZip: ".ZIPをダウンロード",
    packageInfo: "パッケージ情報",
    author: "作者",
    dependencies: "依存関係",
    keywords: "キーワード",
    license: "ライセンス",
    learnMore: "詳細を見る",
    poweredBy: 'Powered by <a href="https://vcc.docs.vrchat.com" target="_blank">VPM</a>',
    builtWith: 'Built with <a href="https://claude.ai" target="_blank">Claude</a>',
  },
  ko: {
    addToVcc: "VCC에 추가",
    copy: "복사",
    howToAddListing: "VCC에 리스팅을 추가하는 방법",
    helpTitle: "이 리스팅을 VCC에 추가하는 방법",
    helpIntro: "이 리스팅을 VCC(VRChat Creator Companion)에 추가하려면 다음 단계를 따라주세요",
    helpStep1: "VCC를 열고 설정으로 이동합니다",
    helpStep2: '"Packages" 탭을 클릭합니다',
    helpStep3: '"Add Repository"를 클릭합니다',
    helpStep4: "표시된 필드에 아래 URL을 붙여넣습니다",
    helpStep5: '"Add"를 클릭합니다',
    helpStep6: '리포지토리 정보를 확인하고 "I Understand"를 클릭합니다',
    helpStep7: "프로젝트를 열면 새로 추가된 리스팅의 패키지를 사용할 수 있습니다.",
    listingUrl: "리스팅 URL",
    docsLink: '패키지 리스팅에 대한 자세한 내용은 <a href="https://vcc.docs.vrchat.com" target="_blank">VCC 문서</a>를 참조하세요',
    searchPlaceholder: "패키지 검색...",
    colName: "이름",
    colType: "종류",
    downloadZip: ".ZIP 다운로드",
    packageInfo: "패키지 정보",
    author: "제작자",
    dependencies: "종속성",
    keywords: "키워드",
    license: "라이선스",
    learnMore: "더 알아보기",
    poweredBy: 'Powered by <a href="https://vcc.docs.vrchat.com" target="_blank">VPM</a>',
    builtWith: 'Built with <a href="https://claude.ai" target="_blank">Claude</a>',
  },
};

const SUPPORTED_LANGS = Object.keys(I18N);

const detectLang = () => {
  try {
    const stored = localStorage.getItem('vpm-lang');
    if (stored && SUPPORTED_LANGS.includes(stored)) return stored;
  } catch (e) {}
  return 'en';
};

const applyI18n = (lang) => {
  const dict = I18N[lang] || I18N.en;
  document.querySelectorAll('[data-i18n]').forEach((el) => {
    const key = el.dataset.i18n;
    if (dict[key] != null) el.textContent = dict[key];
  });
  document.querySelectorAll('[data-i18n-html]').forEach((el) => {
    const key = el.dataset.i18nHtml;
    if (dict[key] != null) el.innerHTML = dict[key];
  });
  document.querySelectorAll('[data-i18n-attr]').forEach((el) => {
    el.dataset.i18nAttr.split(',').forEach((pair) => {
      const [attr, key] = pair.split(':').map((s) => s.trim());
      if (attr && key && dict[key] != null) el.setAttribute(attr, dict[key]);
    });
  });
  document.documentElement.lang = lang;
  try { localStorage.setItem('vpm-lang', lang); } catch (e) {}
};


(() => {
  setTheme();

  window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () => {
    setTheme();
  });

  // Initial i18n pass + language switcher.
  // Fluent components upgrade asynchronously, so we re-apply once they're defined
  // to make sure attributes like placeholder are picked up by the upgraded element.
  const initialLang = detectLang();
  applyI18n(initialLang);
  if (window.customElements) {
    window.customElements.whenDefined('fluent-text-field').then(() => applyI18n(initialLang));
    window.customElements.whenDefined('fluent-select').then(() => {
      const sel = document.getElementById('langSelect');
      if (sel) {
        sel.value = initialLang;
        sel.addEventListener('change', (e) => applyI18n(e.target.value));
      }
    });
  }

  const packageGrid = document.getElementById('packageGrid');

  const searchInput = document.getElementById('searchInput');
  searchInput.addEventListener('input', ({ target: { value = '' }}) => {
    const items = packageGrid.querySelectorAll('fluent-data-grid-row[row-type="default"]');
    items.forEach(item => {
      if (value === '') {
        item.style.display = 'grid';
        return;
      }
      if (
        item.dataset?.packageName?.toLowerCase()?.includes(value.toLowerCase()) ||
        item.dataset?.packageId?.toLowerCase()?.includes(value.toLowerCase())
      ) {
        item.style.display = 'grid';
      } else {
        item.style.display = 'none';
      }
    });
  });

  const urlBarHelpButton = document.getElementById('urlBarHelp');
  const addListingToVccHelp = document.getElementById('addListingToVccHelp');
  urlBarHelpButton.addEventListener('click', () => {
    addListingToVccHelp.hidden = false;
  });
  const addListingToVccHelpClose = document.getElementById('addListingToVccHelpClose');
  addListingToVccHelpClose.addEventListener('click', () => {
    addListingToVccHelp.hidden = true;
  });

  const vccListingInfoUrlFieldCopy = document.getElementById('vccListingInfoUrlFieldCopy');
  vccListingInfoUrlFieldCopy.addEventListener('click', () => {
    const vccUrlField = document.getElementById('vccListingInfoUrlField');
    vccUrlField.select();
    navigator.clipboard.writeText(vccUrlField.value);
    vccListingInfoUrlFieldCopy.appearance = 'accent';
    setTimeout(() => {
      vccListingInfoUrlFieldCopy.appearance = 'neutral';
    }, 1000);
  });

  const vccAddRepoButton = document.getElementById('vccAddRepoButton');
  vccAddRepoButton.addEventListener('click', () => window.location.assign(`vcc://vpm/addRepo?url=${encodeURIComponent(LISTING_URL)}`));

  const vccUrlFieldCopy = document.getElementById('vccUrlFieldCopy');
  vccUrlFieldCopy.addEventListener('click', () => {
    const vccUrlField = document.getElementById('vccUrlField');
    vccUrlField.select();
    navigator.clipboard.writeText(vccUrlField.value);
    vccUrlFieldCopy.appearance = 'accent';
    setTimeout(() => {
      vccUrlFieldCopy.appearance = 'neutral';
    }, 1000);
  });

  const rowMoreMenu = document.getElementById('rowMoreMenu');
  const hideRowMoreMenu = e => {
    if (rowMoreMenu.contains(e.target)) return;
    document.removeEventListener('click', hideRowMoreMenu);
    rowMoreMenu.hidden = true;
  }

  const rowMenuButtons = document.querySelectorAll('.rowMenuButton');
  rowMenuButtons.forEach(button => {
    button.addEventListener('click', e => {
      if (rowMoreMenu?.hidden) {
        rowMoreMenu.style.top = `${e.clientY + e.target.clientHeight}px`;
        rowMoreMenu.style.left = `${e.clientX - 120}px`;
        rowMoreMenu.hidden = false;

        const downloadLink = rowMoreMenu.querySelector('#rowMoreMenuDownload');
        const downloadListener = () => {
          window.open(e?.target?.dataset?.packageUrl, '_blank');
        }
        downloadLink.addEventListener('change', () => {
          downloadListener();
          downloadLink.removeEventListener('change', downloadListener);
        });

        setTimeout(() => {
          document.addEventListener('click', hideRowMoreMenu);
        }, 1);
      }
    });
  });

  const packageInfoModal = document.getElementById('packageInfoModal');
  const packageInfoModalClose = document.getElementById('packageInfoModalClose');
  packageInfoModalClose.addEventListener('click', () => {
    packageInfoModal.hidden = true;
  });

  // Fluent dialogs use nested shadow-rooted elements, so we need to use JS to style them
  const modalControl = packageInfoModal.shadowRoot.querySelector('.control');
  modalControl.style.maxHeight = "90%";
  modalControl.style.transition = 'height 0.2s ease-in-out';
  modalControl.style.overflowY = 'hidden';

  const packageInfoName = document.getElementById('packageInfoName');
  const packageInfoId = document.getElementById('packageInfoId');
  const packageInfoVersion = document.getElementById('packageInfoVersion');
  const packageInfoDescription = document.getElementById('packageInfoDescription');
  const packageInfoAuthor = document.getElementById('packageInfoAuthor');
  const packageInfoDependencies = document.getElementById('packageInfoDependencies');
  const packageInfoKeywords = document.getElementById('packageInfoKeywords');
  const packageInfoLicense = document.getElementById('packageInfoLicense');

  const rowAddToVccButtons = document.querySelectorAll('.rowAddToVccButton');
  rowAddToVccButtons.forEach((button) => {
    button.addEventListener('click', () => window.location.assign(`vcc://vpm/addRepo?url=${encodeURIComponent(LISTING_URL)}`));
  });

  const rowPackageInfoButton = document.querySelectorAll('.rowPackageInfoButton');
  rowPackageInfoButton.forEach((button) => {
    button.addEventListener('click', e => {
      const packageId = e.target.dataset?.packageId;
      const packageInfo = PACKAGES?.[packageId];
      if (!packageInfo) {
        console.error(`Did not find package ${packageId}. Packages available:`, PACKAGES);
        return;
      }

      packageInfoName.textContent = packageInfo.displayName;
      packageInfoId.textContent = packageId;
      packageInfoVersion.textContent = `v${packageInfo.version}`;
      packageInfoDescription.textContent = packageInfo.description;
      packageInfoAuthor.textContent = packageInfo.author.name;
      packageInfoAuthor.href = packageInfo.author.url;

      if ((packageInfo.keywords?.length ?? 0) === 0) {
        packageInfoKeywords.parentElement.classList.add('hidden');
      } else {
        packageInfoKeywords.parentElement.classList.remove('hidden');
        packageInfoKeywords.innerHTML = null;
        packageInfo.keywords.forEach(keyword => {
          const keywordDiv = document.createElement('div');
          keywordDiv.classList.add('me-2', 'mb-2', 'badge');
          keywordDiv.textContent = keyword;
          packageInfoKeywords.appendChild(keywordDiv);
        });
      }

      if (!packageInfo.license?.length && !packageInfo.licensesUrl?.length) {
        packageInfoLicense.parentElement.classList.add('hidden');
      } else {
        packageInfoLicense.parentElement.classList.remove('hidden');
        packageInfoLicense.textContent = packageInfo.license ?? 'See License';
        packageInfoLicense.href = packageInfo.licensesUrl ?? '#';
      }

      packageInfoDependencies.innerHTML = null;
      Object.entries(packageInfo.dependencies).forEach(([name, version]) => {
        const depRow = document.createElement('li');
        depRow.classList.add('mb-2');
        depRow.textContent = `${name} @ v${version}`;
        packageInfoDependencies.appendChild(depRow);
      });

      packageInfoModal.hidden = false;

      setTimeout(() => {
        const height = packageInfoModal.querySelector('.col').clientHeight;
        modalControl.style.setProperty('--dialog-height', `${height + 14}px`);
      }, 1);
    });
  });

  const packageInfoVccUrlFieldCopy = document.getElementById('packageInfoVccUrlFieldCopy');
  packageInfoVccUrlFieldCopy.addEventListener('click', () => {
    const vccUrlField = document.getElementById('packageInfoVccUrlField');
    vccUrlField.select();
    navigator.clipboard.writeText(vccUrlField.value);
    packageInfoVccUrlFieldCopy.appearance = 'accent';
    setTimeout(() => {
      packageInfoVccUrlFieldCopy.appearance = 'neutral';
    }, 1000);
  });

  const packageInfoListingHelp = document.getElementById('packageInfoListingHelp');
  packageInfoListingHelp.addEventListener('click', () => {
    addListingToVccHelp.hidden = false;
  });
})();